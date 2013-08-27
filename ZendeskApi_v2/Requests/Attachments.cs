using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
using System.Net.Http;
#endif
using Newtonsoft.Json;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public class Attachments : Core
    {
        internal Attachments(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
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
            var requestUrl = String.Format("https://{0}.zendesk.com/api/v2/uploads.json?filename={1}", ConnectionSettings.Domain, file.FileName);

            if (!string.IsNullOrEmpty(token))
                requestUrl += string.Format("&token={0}", token);

            WebRequest req = WebRequest.Create(requestUrl);
            req.ContentType = file.ContentType;
            req.Method = "POST";
            req.ContentLength = file.FileData.Length;
            req.Credentials = ConnectionSettings.Credentials.CreateNetworkCredentials();
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

            var res = await UploadAttachmentAsync(files[0]);

            if (files.Count > 1)
            {
                var otherFiles = files.Skip(1);
                foreach (var file in otherFiles)
                    res = await UploadAttachmentAsync(file, res.Token);
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
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "")
        {

            var requestUrl = String.Format("https://{0}.zendesk.com/api/v2/uploads.json?filename={1}", ConnectionSettings.Domain, file.FileName);

            if (!string.IsNullOrEmpty(token))
                requestUrl += string.Format("&token={0}", token);

            using (
                var handler = new HttpClientHandler
                {
                    Credentials = ConnectionSettings.Credentials.CreateNetworkCredentials(),
                    PreAuthenticate = true
                })
            {
                using (var client = new HttpClient(handler))
                {
                    var req = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    req.Headers.Add("Accept", "application/json");

                    req.Content = new ByteArrayContent(file.FileData);
                    return (await (await client.SendAsync(req)).Content.ReadAsStringAsync()).ConvertToObject<UploadResult>().Upload;
                
                }
            }
        }

#endif
    }
}
