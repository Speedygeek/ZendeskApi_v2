using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
#if ASYNC
using System.Threading.Tasks;
using System.Net.Http;
#endif
using Newtonsoft.Json;

namespace ZendeskApi_v2
{
    public class Core
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";
        protected internal IZendeskConnectionSettings ConnectionSettings { get; private set; }

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="connectionSettings"></param>
        internal Core(IZendeskConnectionSettings connectionSettings)
        {
            ConnectionSettings = connectionSettings;
        }

#if SYNC
        public T GetByPageUrl<T>(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return JsonConvert.DeserializeObject<T>("");

            var resource = Regex.Split(pageUrl, "api/v2/").Last();
            return RunRequest<T>(resource, "GET");
        }

        private T RunRequest<T>(string resource, string requestMethod, object body = null)
        {
            var response = RunRequest(resource, requestMethod, body);
            var obj = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            return obj;
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null)
        {
            var requestUrl = String.Format("https://{0}.zendesk.com/api/v2/{1}", ConnectionSettings.Domain, resource);

            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
            req.ContentType = "application/json";

            req.Credentials = ConnectionSettings.Credentials.CreateNetworkCredentials();
            //req.Headers["Authorization"] = GetAuthHeader(User, Password);  //why?
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

        protected T GenericPost<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, "POST", body);
            return res;
        }

        protected bool GenericBoolPost(string resource, object body = null)
        {
            var res = RunRequest(resource, "POST", body);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        protected T GenericPut<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, "PUT", body);
            return res;
        }

        protected bool GenericBoolPut(string resource, object body = null)
        {
            var res = RunRequest(resource, "PUT", body);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }
#endif

        protected string GetAuthHeader(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));
            return string.Format("Basic {0}", auth);
        }

#if ASYNC
        public async Task<T> GetByPageUrlAsync<T>(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return JsonConvert.DeserializeObject<T>("");

            var resource = Regex.Split(pageUrl, "api/v2/").Last();
            return await RunRequestAsync<T>(resource, "GET");
        }


        protected async Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null)
        {
            var response = await RunRequestAsync(resource, requestMethod, body);
            var obj = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response.Content));
            return await obj;
        }

        protected async Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null)
        {
            var requestUrl = String.Format("https://{0}.zendesk.com/api/v2/{1}", ConnectionSettings.Domain, resource);

            using (
                var handler = new HttpClientHandler
                {
                    Credentials = ConnectionSettings.Credentials.CreateNetworkCredentials(),
                    PreAuthenticate = true
                })
            {
                using (var client = new HttpClient(handler))
                {
                    var req = new HttpRequestMessage(new HttpMethod(requestMethod), requestUrl);
                    req.Headers.Add("Accept", "application/json");

                    if (body != null)
                    {
                        await Task.Factory.StartNew(() =>
                            {
                                req.Content = new StringContent(
                                    JsonConvert.SerializeObject(body,
                                                                new JsonSerializerSettings
                                                                    {
                                                                        NullValueHandling = NullValueHandling.Ignore
                                                                    }),
                                    Encoding.UTF8, "application/json");
                            });
                    }

                    var response = await client.SendAsync(req);

                    return new RequestResult
                        {
                        Content = await response.Content.ReadAsStringAsync(),
                        HttpStatusCode = response.StatusCode
                    };
                }
            }
        }

        protected async Task<T> GenericGetAsync<T>(string resource)
        {
            return await RunRequestAsync<T>(resource, "GET");
        }

        protected async Task<bool> GenericDeleteAsync(string resource)
        {
            var res = RunRequestAsync(resource, "DELETE");
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        protected async Task<T> GenericPostAsync<T>(string resource, object body = null)
        {
            var res = RunRequestAsync<T>(resource, "POST", body);
            return await res;
        }

        protected async Task<bool> GenericBoolPostAsync(string resource, object body = null)
        {
            var res = RunRequestAsync(resource, "POST", body);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        protected async Task<T> GenericPutAsync<T>(string resource, object body = null)
        {
            var res = RunRequestAsync<T>(resource, "PUT", body);
            return await res;
        }

        protected async Task<bool> GenericBoolPutAsync(string resource, object body = null)
        {
            var res = RunRequestAsync(resource, "PUT", body);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }
#endif
    }
}
