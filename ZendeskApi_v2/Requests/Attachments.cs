using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public class Attachments : Core
    {
        public Attachments(string yourZendeskUrl, string user, string password) : base(yourZendeskUrl, user, password)
        { }
#if SYNC
        public Upload UploadAttachment(ZenFile file)
        {
            return UploadAttachment(file, "");
        }

        public Upload UploadAttachments(List<ZenFile> files)
        {
            if (files.Count < 1)
                return null;

            var res = UploadAttachment(files[0]);

            if (files.Count > 1)
            {
                var otherFiles = files.Skip(1);
                foreach (var curFile in otherFiles)
                    res = UploadAttachment(curFile, res.Token);
            }

            return res;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <returns></returns>       
        Upload UploadAttachment(ZenFile file, string token = "")
        {
            var requestUrl = ZendeskUrl;
            if (!requestUrl.EndsWith("/"))
                requestUrl += "/";

            requestUrl += string.Format("uploads.json?filename={0}", file.FileName);
            if (!string.IsNullOrEmpty(token))
                requestUrl += string.Format("&token={0}", token);

            WebRequest req = WebRequest.Create(requestUrl);
            req.ContentType = file.ContentType;
            req.Method = "POST";
            req.ContentLength = file.FileData.Length;
            var credentials = new System.Net.CredentialCache
                                  {
                                      {
                                          new System.Uri(ZendeskUrl), "Basic",
                                          new System.Net.NetworkCredential(User, Password)
                                          }
                                  };

            req.Credentials = credentials;
            req.PreAuthenticate = true;
            //req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequired;
            var dataStream = req.GetRequestStream();
            dataStream.Write(file.FileData, 0, file.FileData.Length);
            dataStream.Close();

            WebResponse response = req.GetResponse();
            dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer.ConvertToObject<UploadResult>().Upload;
        }     
#endif

#if ASYNC
        public async Task<Upload> UploadAttachmentAsync(ZenFile file)
        {
            return await UploadAttachmentAsync(file, "");
        }

        public async Task<Upload> UploadAttachmentsAsync(List<ZenFile> files)
        {
            if (files.Count < 1)
                return null;

            var res = UploadAttachmentAsync(files[0]);

            if (files.Count > 1)
            {
                var otherFiles = files.Skip(1);
                foreach (var curFile in otherFiles)
                {
                    res = await res.ContinueWith(x =>  UploadAttachmentAsync(curFile, x.Result.Token));                    
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
        /// <returns></returns>  
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "")
        {
            var requestUrl = ZendeskUrl;
            if (!requestUrl.EndsWith("/"))
                requestUrl += "/";

            requestUrl += string.Format("uploads.json?filename={0}", file.FileName);
            if (!string.IsNullOrEmpty(token))
                requestUrl += string.Format("&token={0}", token);


            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.ContentType = file.ContentType;
            req.Credentials = new System.Net.NetworkCredential(User, Password);
            req.Headers["Authorization"] = GetAuthHeader(User, Password);
            req.Method = "POST"; //GET POST PUT DELETE

            req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";                                        
            
            var requestStream = Task.Factory.FromAsync(
                req.BeginGetRequestStream,
                asyncResult => req.EndGetRequestStream(asyncResult),
                (object)null);
            
            var dataStream = await requestStream.ContinueWith(t => t.Result.WriteAsync(file.FileData, 0, file.FileData.Length));
            Task.WaitAll(dataStream);
            

            Task<WebResponse> task = Task.Factory.FromAsync(
            req.BeginGetResponse,
            asyncResult => req.EndGetResponse(asyncResult),
            (object)null);

            return await task.ContinueWith(t =>
            {
                var httpWebResponse = t.Result as HttpWebResponse;
                return ReadStreamFromResponse(httpWebResponse).ConvertToObject<UploadResult>().Upload;
            });
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
