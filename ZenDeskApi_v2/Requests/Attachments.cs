using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Shared;

namespace ZenDeskApi_v2.Requests
{
    public class Attachments : Core
    {
        public Attachments(string yourZenDeskUrl, string user, string password) : base(yourZenDeskUrl, user, password)
        { }

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
            var requestUrl = ZenDeskUrl;
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
                                          new System.Uri(ZenDeskUrl), "Basic",
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
    }
}
