using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Attachments;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public interface IAttachments : ICore
    {
#if SYNC
        GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId);
        Upload UploadAttachment(ZenFile file, int? timeout = null);
        Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null);
#endif

#if ASYNC
        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>  
        Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "", int? timeout = null);
        Task<Upload> UploadAttachmentsAsync(IEnumerable<ZenFile> files, int? timeout = null);
#endif
    }

    public class Attachments : Core, IAttachments
    {
        public Attachments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        { }
#if SYNC

        public GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId)
        {
            return GenericGet<GroupAttachmentResponse>(string.Format("help_center/articles/{0}/attachments.json", articleId));
        }


        public Upload UploadAttachment(ZenFile file, int? timeout = null)
        {
            return UploadAttachment(file, "", timeout);
        }

        public Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
                return null;

            var res = UploadAttachment(zenFiles.First(), timeout);

            if (zenFiles.Count() > 1)
            {
                var otherFiles = zenFiles.Skip(1);
                foreach (var curFile in otherFiles)
                    res = UploadAttachment(curFile, res.Token, timeout);
            }

            return res;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>       
        Upload UploadAttachment(ZenFile file, string token, int? timeout = null)
        {
            var resource = string.Format("uploads.json?filename={0}", file.FileName);
            if (!string.IsNullOrEmpty(token))
                    resource += string.Format("&token={0}", token);

            var requestResult = RunRequest<UploadResult>(resource, RequestMethod.POST.ToString(), file, timeout);
            return requestResult.Upload;
        }
#endif

#if ASYNC
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, int? timeout = null)
        {
            return await UploadAttachmentAsync(file, "", timeout);
        }

        public async Task<Upload> UploadAttachmentsAsync(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
                return null;

            var res = UploadAttachmentAsync(zenFiles.First(), timeout);

            if (zenFiles.Count() > 1)
            {
                var otherFiles = zenFiles.Skip(1);
                foreach (var curFile in otherFiles)
                {
                    var file = curFile;
                    res = await res.ContinueWith(x => UploadAttachmentAsync(file, x.Result.Token, timeout));
                }
            }

            return await res;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>  
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "", int? timeout = null)
        {
            try
            {
                var requestUrl = ZendeskUrl + string.Format("uploads.json?filename={0}", file.FileName);

                if (!string.IsNullOrEmpty(token))
                    requestUrl += string.Format("&token={0}", token);

                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = file.ContentType;
                req.Headers["Authorization"] = GetPasswordOrTokenAuthHeader();
                req.Method = "POST"; //GET POST PUT DELETE

                req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";

                var requestStream = Task.Factory.FromAsync(
                    req.BeginGetRequestStream,
                    asyncResult => req.EndGetRequestStream(asyncResult),
                    (object) null);

                var dataStream =
                    await requestStream.ContinueWith(t => t.Result.WriteAsync(file.FileData, 0, file.FileData.Length));
                Task.WaitAll(dataStream);

                Task<WebResponse> task = Task.Factory.FromAsync(
                    req.BeginGetResponse,
                    asyncResult => req.EndGetResponse(asyncResult),
                    (object) null);

                return await task.ContinueWith(t =>
                {
                    var httpWebResponse = t.Result as HttpWebResponse;
                    return ReadStreamFromResponse(httpWebResponse).ConvertToObject<UploadResult>().Upload;
                });
            }
            catch (WebException ex)
            {
                string error = string.Empty;
                if (ex.Response != null || (ex.InnerException is WebException && ((WebException)(ex.InnerException)).Response != null))
                    using (Stream stream = (ex.Response ?? ((WebException)ex.InnerException).Response).GetResponseStream())

                        if (stream != null)
                        {
                            using (var sr = new StreamReader(stream))
                            {
                                error = sr.ReadToEnd();
                            }
                        }
                        else
                        {
                            error = "Cannot read error stream.";
                        }

                Debug.WriteLine(ex.Message);
                Debug.WriteLine(error);

               var  headers = ("Error Content: " + error) + "\r\n" + (" File Name: " + (file.FileName ?? string.Empty) + "\r\n" + " File Length: " + (file.FileData != null ? file.FileData.Length.ToString() : "no data") + "\r\n");

                if (ex.Response != null && ex.Response.Headers != null)
                    headers += ex.Response.Headers;   
             
                var wException = new WebException(ex.Message + headers, ex);
                wException.Data.Add("jsonException", error);

                throw wException;
            }
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }
#endif


    }
}
