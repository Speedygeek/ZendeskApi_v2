using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace ZendeskApi_v2
{
    public class Core
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";        
        protected string User;
        protected string Password;
        protected string ZendeskUrl;        
       
        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zendeskApiUrl"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Core(string zendeskApiUrl, string user, string password)
        {
            User = user;
            Password = password;
            ZendeskUrl = zendeskApiUrl;            
        }

        public T RunRequest<T>(string resource, string requestMethod, object body = null)
        {
            var response = RunRequest(resource, requestMethod, body);
            var obj = JsonConvert.DeserializeObject<T>(response.Content);
            return obj;
        }

        protected string GetAuthHeader(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));            
            return string.Format("Basic {0}", auth);
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null)
        {
            var requestUrl = ZendeskUrl;
            if (!requestUrl.EndsWith("/"))
                requestUrl += "/";

            requestUrl += resource;                        

            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.ContentType = "application/json";
            
            req.Credentials = new System.Net.CredentialCache
                                  {
                                      {
                                          new System.Uri(ZendeskUrl), "Basic",
                                          new System.Net.NetworkCredential(User, Password)
                                          }
                                  };
            req.Headers["Authorization"] = GetAuthHeader(User, Password);
            req.PreAuthenticate = true;

            req.Method = requestMethod; //GET POST PUT DELETE
            req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";
            req.ContentLength = 0;

            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                byte[] formData = UTF8Encoding.UTF8.GetBytes(json);
                req.ContentLength = formData.Length;

                var dataStream = req.GetRequestStream();
                dataStream.Write(formData, 0, formData.Length);
                dataStream.Close();
            }
            var res = req.GetResponse();
            HttpWebResponse response = res as HttpWebResponse;
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            string responseFromServer = reader.ReadToEnd();

            return new RequestResult()
            {
                Content = responseFromServer,
                HttpStatusCode = response.StatusCode
            };
        }

        protected T GenericGet<T>(string resource)
        {
            return RunRequest<T>(resource, "GET");            
        }
        
        protected bool GenericDelete(string resource)
        {
            var res = RunRequest(resource, "DELETE");
            return res.HttpStatusCode == HttpStatusCode.OK;            
        }
        
        protected T GenericPost<T>(string resource, object body=null)
        {
            var res = RunRequest<T>(resource, "POST", body);            
            return res;
        }
        
        protected T GenericPut<T>(string resource, object body=null)
        {
            var res = RunRequest<T>(resource, "PUT", body);
            return res;
        }       
    }
}
