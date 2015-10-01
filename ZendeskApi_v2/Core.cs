using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
#if ASYNC
using System.Threading.Tasks;
#endif
using Newtonsoft.Json;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2
{
    public enum RequestMethod
    {
        GET,
        PUT,
        POST,
        DELETE
    }

    public interface ICore
    {
#if SYNC
        T GetByPageUrl<T>(string pageUrl, int perPage = 100);
        T RunRequest<T>(string resource, string requestMethod, object body = null, int? timeout = null);
        RequestResult RunRequest(string resource, string requestMethod, object body = null, int? timeout = null);
#endif

#if ASYNC
        Task<T> GetByPageUrlAsync<T>(string pageUrl, int perPage = 100);
        Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null, int? timeout = null);
        Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null, int? timeout = null);
#endif
    }

    public class Core : ICore
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";
        protected string User;
        protected string Password;
        protected string ZendeskUrl;
        protected string ApiToken;
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        protected string OAuthToken;

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zendeskApiUrl"></param>
        /// <param name="p_OAuthToken"></param>
        public Core(string zendeskApiUrl, string p_OAuthToken) :
            this(zendeskApiUrl, null, null, null, p_OAuthToken)
        {
        }

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zendeskApiUrl"></param>
        /// <param name="p_OAuthToken"></param>
        public Core(string zendeskApiUrl, string user, string password, string apiToken) :
            this(zendeskApiUrl, user, password, apiToken, null)
        {
        }

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zendeskApiUrl"></param>
        /// <param name="user"></param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Optional Param which is used if specified instead of the password</param>
        public Core(string zendeskApiUrl, string user, string password, string apiToken, string p_OAuthToken)
        {
            User = user;
            Password = password;
            if (!zendeskApiUrl.EndsWith("/", StringComparison.CurrentCulture))
                zendeskApiUrl += "/";

            ZendeskUrl = zendeskApiUrl;
            ApiToken = apiToken;
            OAuthToken = p_OAuthToken;
        }

#if SYNC
        internal IWebProxy Proxy;

        public T GetByPageUrl<T>(string pageUrl, int perPage = 100)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return JsonConvert.DeserializeObject<T>("");

            var resource = Regex.Split(pageUrl, "api/v2/").Last() + "&per_page=" + perPage;
            return RunRequest<T>(resource, "GET");
        }

        public T RunRequest<T>(string resource, string requestMethod, object body = null, int? timeout = null)
        {
            var response = RunRequest(resource, requestMethod, body, timeout);
            var obj = JsonConvert.DeserializeObject<T>(response.Content, jsonSettings);
            return obj;
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null, int? timeout = null)
        {
            try
            {
                var requestUrl = ZendeskUrl + resource;
                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = "application/json";

                if (this.Proxy != null)
                    req.Proxy = this.Proxy;

                req.Headers["Authorization"] = GetPasswordOrTokenAuthHeader();
                req.PreAuthenticate = true;

                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";
                req.Timeout = timeout ?? req.Timeout;

                if (body != null)
                {
                    byte[] data = null;
                    if (body is ZenFile)
                    {
                        req.ContentType = ((ZenFile)body).ContentType;
                        data = ((ZenFile)body).FileData;
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(body, jsonSettings);
                        data = Encoding.UTF8.GetBytes(json);
                    }

                    req.ContentLength = data.Length;

                    using (var dataStream = req.GetRequestStream())
                    {
                        dataStream.Write(data, 0, data.Length);
                    }
                }

                var res = req.GetResponse();
                var response = res as HttpWebResponse;
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                string responseFromServer = reader.ReadToEnd();

                return new RequestResult()
                {
                    Content = responseFromServer,
                    HttpStatusCode = response.StatusCode
                };
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
                Debug.Write(ex.Message);
                Debug.Write(error);

                var headers = ("Error Content: " + error) + "\r\n" + " Resource String: " + resource + "\r\n" + ((body != null && !(body is ZenFile)) ? " Body: " + JsonConvert.SerializeObject(body) : "") + "\r\n";
                if (body != null && (body is ZenFile))
                    headers = ("Error Content: " + error) + "\r\n" +(" File Name: " + (((ZenFile) body).FileName ?? string.Empty) + "\r\n" +
                        " File Length: " + (((ZenFile)body).FileData != null ? ((ZenFile)body).FileData.Length.ToString() : "no data") + "\r\n");
                
                if (ex.Response != null && ex.Response.Headers != null)
                    headers += ex.Response.Headers;

                var wException = new WebException(ex.Message + headers, ex);
                wException.Data.Add("jsonException", error);

                throw wException;
            }
        }

        protected T GenericGet<T>(string resource)
        {
            return RunRequest<T>(resource, "GET");
        }

        protected T GenericPagedGet<T>(string resource, int? perPage = null, int? page = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.Any())
            {
                paramString = (resource.Contains("?") ? "&" : "?") + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value).ToArray());
            }


            return GenericGet<T>(resource + paramString);
        }

        protected T GenericPagedSortedGet<T>(string resource, int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(sortCol))
            {
                parameters.Add("sort_by", sortCol);
            }

            if (sortAscending.HasValue)
            {
                parameters.Add("sort_order", sortAscending.Value ? "" : "desc");
            }

            if (parameters.Any())
            {
                paramString = (resource.Contains("?") ? "&" : "?") + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value).ToArray());
            }


            return GenericGet<T>(resource + paramString);
        }

        protected bool GenericDelete(string resource)
        {
            var res = RunRequest(resource, "DELETE");
            return res.HttpStatusCode == HttpStatusCode.OK || res.HttpStatusCode == HttpStatusCode.NoContent;
        }

        protected T GenericDelete<T>(string resource)
        {
            var res = RunRequest<T>(resource, "DELETE");
            return res;
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

        protected string GetPasswordOrTokenAuthHeader()
        {
            if (!ApiToken.IsNullOrWhiteSpace() && !User.IsNullOrWhiteSpace())
                return GetAuthHeader(User + "/token", ApiToken);
            else if (!Password.IsNullOrWhiteSpace() && !User.IsNullOrWhiteSpace())
                return GetAuthHeader(User, Password);
            else if (!OAuthToken.IsNullOrWhiteSpace())
                return GetAuthBearerHeader(OAuthToken);
            else
                return string.Empty;
        }

        protected string GetAuthBearerHeader(string oAuthToken)
        {
            return string.Format("Bearer {0}", oAuthToken);
        }

        protected string GetAuthHeader(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));
            return string.Format("Basic {0}", auth);
        }

#if ASYNC
        public async Task<T> GetByPageUrlAsync<T>(string pageUrl, int perPage = 100)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return JsonConvert.DeserializeObject<T>("");

            var resource = Regex.Split(pageUrl, "api/v2/").Last() + "&per_page=" + perPage;
            return await RunRequestAsync<T>(resource, "GET");
        }

        public async Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null, int? timeout = null)
        {
            var response = await RunRequestAsync(resource, requestMethod, body, timeout);
            var obj = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response.Content));
            return await obj;
        }

        public async Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null, int? timeout = null)
        {
            var requestUrl = ZendeskUrl + resource;
            try
            {
                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;
                req.ContentType = "application/json";

                req.Headers["Authorization"] = GetPasswordOrTokenAuthHeader();
                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";

                if (body != null)
                {
                    var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body, jsonSettings));

                    using (Stream requestStream = await req.GetRequestStreamAsync())
                    using (StreamWriter writer = new StreamWriter(requestStream))
                    {
                        await writer.BaseStream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                    }
                }

                string content = string.Empty;
                using (WebResponse response = await req.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        content = await sr.ReadToEndAsync();
                    }

                    var httpResponse = (HttpWebResponse) response;
                    return new RequestResult {HttpStatusCode = httpResponse.StatusCode, Content = content};
                }
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
                Debug.Write(ex.Message);
                Debug.Write(error);

                var headers = ("Error Content: " + error) + "\r\n" + " Resource String: " + resource + "\r\n" + ((body != null && !(body is ZenFile)) ? " Body: " + JsonConvert.SerializeObject(body) : "") + "\r\n";
                if (ex.Response != null && ex.Response.Headers != null)
                    headers += ex.Response.Headers;
                
                var wException = new WebException(ex.Message + headers, ex);
                wException.Data.Add("jsonException", error);

                throw wException;
            }
        }

        protected async Task<T> GenericGetAsync<T>(string resource)
        {
            return await RunRequestAsync<T>(resource, "GET");
        }

        protected async Task<T> GenericPagedGetAsync<T>(string resource, int? perPage = null, int? page = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.Any())
            {
                paramString = (resource.Contains("?") ? "&" : "?") + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));
            }


            return await GenericGetAsync<T>(resource + paramString);
        }

        protected async Task<T> GenericPagedSortedGetAsync<T>(string resource, int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            var parameters = new Dictionary<string, string>();

            var paramString = "";
            if (perPage.HasValue)
            {
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (page.HasValue)
            {
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(sortCol))
            {
                parameters.Add("sort_by", sortCol);
            }

            if (sortAscending.HasValue)
            {
                parameters.Add("sort_order", sortAscending.Value ? "" : "desc");
            }

            if (parameters.Any())
            {
                paramString = (resource.Contains("?") ? "&" : "?") + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));
            }


            return await GenericGetAsync<T>(resource + paramString);
        }


        protected async Task<bool> GenericDeleteAsync(string resource)
        {
            var res = RunRequestAsync(resource, "DELETE");
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK || x.Result.HttpStatusCode == HttpStatusCode.NoContent);
        }

        protected async Task<T> GenericDeleteAsync<T>(string resource)
        {
            var res = RunRequestAsync<T>(resource, "DELETE");
            return await res;
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
