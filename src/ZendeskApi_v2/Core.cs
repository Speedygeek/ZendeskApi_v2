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

    public static class RequestMethod
    {
        public const string Get = "GET";
        public const string Put = "PUT";
        public const string Post = "POST";
        public const string Delete = "DELETE";
    }


    public interface ICore
    {
#if SYNC
        T GetByPageUrl<T>(string pageUrl, int perPage = 100);
        T RunRequest<T>(string resource, string requestMethod, object body = null, int? timeout = null, string formFile = null);
        RequestResult RunRequest(string resource, string requestMethod, object body = null, int? timeout = null, string formFile = null);
#endif

#if ASYNC
        Task<T> GetByPageUrlAsync<T>(string pageUrl, int perPage = 100);
        Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null, int? timeout = null, string formFile = null);
        Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null, int? timeout = null, string formFile = null);
#endif
    }

    public class Core : ICore
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";
        protected string User;
        protected string Password;
        protected string ZendeskUrl;
        protected string ApiToken;
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateParseHandling = DateParseHandling.DateTimeOffset, DateFormatHandling = DateFormatHandling.IsoDateFormat, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate };
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
            {
                zendeskApiUrl += "/";
            }

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
            return RunRequest<T>(resource, RequestMethod.Get);
        }

        public T RunRequest<T>(string resource, string requestMethod, object body = null, int? timeout = null, string formKey = null)
        {
            var response = RunRequest(resource, requestMethod, body, timeout, formKey);
            var obj = JsonConvert.DeserializeObject<T>(response.Content, jsonSettings);
            return obj;
        }

        public RequestResult RunRequest(string resource, string requestMethod, object body = null, int? timeout = null, string formKey = null)
        {
            try
            {
                string requestUrl = ZendeskUrl + resource;

                HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;

                if (this.Proxy != null)
                {
                    req.Proxy = this.Proxy;
                }

                req.Headers["Authorization"] = GetPasswordOrTokenAuthHeader();
                req.PreAuthenticate = true;

                req.Method = requestMethod; //GET POST PUT DELETE
                req.Accept = "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml";
                req.Timeout = timeout ?? req.Timeout;

                if (body != null)
                {
                    byte[] data = null;
                    var zenFile = body as ZenFile;
                    if (zenFile == null)
                    {
                        req.ContentType = "application/json";
                        data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body, jsonSettings));
                    }
                    else
                    {
                        req.ContentType = zenFile.ContentType;
                        data = formKey.IsNullOrWhiteSpace() ? zenFile.FileData : GetFromData(zenFile, req, formKey);
                    }

                    req.ContentLength = data.Length;

                    using (var dataStream = req.GetRequestStream())
                    {
                        dataStream.Write(data, 0, data.Length);
                    }
                }

                var res = req.GetResponse();
                var response = res as HttpWebResponse;
                string responseFromServer = string.Empty;
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }

                return new RequestResult { Content = responseFromServer, HttpStatusCode = response.StatusCode };
            }
            catch (WebException ex)
            {
                WebException wException = GetWebException(resource, body, ex);
                throw wException;
            }
        }

        private byte[] GetFromData(ZenFile zenFile, HttpWebRequest req, string formKey)
        {
            string boundaryString = "FEF3F395A90B452BB8BFDC878DDBD152";
            req.ContentType = "multipart/form-data; boundary=" + boundaryString;
            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            // Include the file in the post data
            postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
            postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                                    formKey, zenFile.FileName, zenFile.ContentType);
            postDataWriter.Flush();
            postDataStream.Write(zenFile.FileData, 0, zenFile.FileData.Length);
            postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
            postDataWriter.Flush();

            return postDataStream.ToArray();
        }

        protected T GenericGet<T>(string resource)
        {
            return RunRequest<T>(resource, RequestMethod.Get);
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
            var res = RunRequest(resource, RequestMethod.Delete);
            return res.HttpStatusCode == HttpStatusCode.OK || res.HttpStatusCode == HttpStatusCode.NoContent;
        }

        protected T GenericDelete<T>(string resource)
        {
            var res = RunRequest<T>(resource, RequestMethod.Delete);
            return res;
        }

        protected T GenericPost<T>(string resource, object body = null)
        {
            var res = RunRequest<T>(resource, RequestMethod.Post, body);
            return res;
        }

        protected bool GenericBoolPost(string resource, object body = null)
        {
            var res = RunRequest(resource, RequestMethod.Post, body);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        protected T GenericPut<T>(string resource, object body = null, string formKey = null)
        {
            var res = RunRequest<T>(resource, RequestMethod.Put, body, formKey: formKey);
            return res;
        }

        protected bool GenericBoolPut(string resource, object body = null)
        {
            var res = RunRequest(resource, RequestMethod.Put, body);
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
            return await RunRequestAsync<T>(resource, RequestMethod.Get);
        }

        public async Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null, int? timeout = null, string formKey = null)
        {
            var response = await RunRequestAsync(resource, requestMethod, body, timeout, formKey);
            var obj = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response.Content));
            return await obj;
        }

        public async Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null, int? timeout = null, string formKey = null)
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
                    byte[] data = null;
                    var zenFile = body as ZenFile;
                    if (zenFile == null)
                    {
                        string json = JsonConvert.SerializeObject(body, jsonSettings);

                        data = Encoding.UTF8.GetBytes(json);
                    }
                    else
                    {
                        req.ContentType = zenFile.ContentType;
                        data = formKey.IsNullOrWhiteSpace() ? zenFile.FileData : await GetFromDataAsync(zenFile, req, formKey);
                    }

                    using (Stream requestStream = await req.GetRequestStreamAsync())
                    {
                        await requestStream.WriteAsync(data, 0, data.Length);
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)await req.GetResponseAsync())
                {
                    string content = string.Empty;
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                        {
                            content = await sr.ReadToEndAsync();
                        }
                    }
                    return new RequestResult { HttpStatusCode = response.StatusCode, Content = content };
                }
            }
            catch (WebException ex)
            {
                WebException wException = GetWebException(resource, body, ex);
                throw wException;
            }
        }

        private async Task<byte[]> GetFromDataAsync(ZenFile zenFile, HttpWebRequest req, string formKey)
        {
            string boundaryString = "FEF3F395A90B452BB8BFDC878DDBD152";
            req.ContentType = "multipart/form-data; boundary=" + boundaryString;
            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            // Include the file in the post data
            await postDataWriter.WriteAsync("\r\n--" + boundaryString + "\r\n");
            await postDataWriter.WriteAsync(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                                    formKey, zenFile.FileName, zenFile.ContentType));
            await postDataWriter.FlushAsync();
            await postDataStream.WriteAsync(zenFile.FileData, 0, zenFile.FileData.Length);
            await postDataWriter.WriteAsync("\r\n--" + boundaryString + "--\r\n");
            await postDataWriter.FlushAsync();

            return postDataStream.ToArray();
        }

        protected async Task<T> GenericGetAsync<T>(string resource)
        {
            return await RunRequestAsync<T>(resource, RequestMethod.Get);
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
            var res = RunRequestAsync(resource, RequestMethod.Delete);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK || x.Result.HttpStatusCode == HttpStatusCode.NoContent);
        }

        protected async Task<T> GenericDeleteAsync<T>(string resource)
        {
            var res = RunRequestAsync<T>(resource, RequestMethod.Delete);
            return await res;
        }

        protected async Task<T> GenericPostAsync<T>(string resource, object body = null)
        {
            var res = RunRequestAsync<T>(resource, RequestMethod.Post, body);
            return await res;
        }

        protected async Task<bool> GenericBoolPostAsync(string resource, object body = null)
        {
            var res = RunRequestAsync(resource, RequestMethod.Post, body);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        protected async Task<T> GenericPutAsync<T>(string resource, object body = null, string formKey = null)
        {
            var res = RunRequestAsync<T>(resource, RequestMethod.Put, body, formKey: formKey);
            return await res;
        }

        protected async Task<bool> GenericBoolPutAsync(string resource, object body = null)
        {
            var res = RunRequestAsync(resource, RequestMethod.Put, body);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }
#endif

        private WebException GetWebException(string resource, object body, WebException originalWebException)
        {
            string error = string.Empty;
            WebException innerException = originalWebException.InnerException as WebException;

            if (originalWebException.Response != null || (innerException != null && innerException.Response != null))
            {
                using (Stream stream = (originalWebException.Response ?? innerException.Response).GetResponseStream())
                {
                    if (stream != null && stream.CanRead)
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
                }
            }
            Debug.WriteLine(originalWebException.Message);
            Debug.WriteLine(error);

            string headersMessage = string.Format("Error content: {0} \r\n Resource String: {1}  + \r\n", error, resource);
            string bodyMessage = string.Empty;

            if (body != null)
            {
                ZenFile zenFile = body as ZenFile;
                if (zenFile == null)
                {
                    bodyMessage = string.Format(" Body: {0}", JsonConvert.SerializeObject(body, Formatting.Indented, jsonSettings));
                }
                else
                {
                    bodyMessage = string.Format(" File Name: {0} \r\n File Length: {1}\r\n", zenFile.FileName,
                        (zenFile.FileData != null ? zenFile.FileData.Length.ToString() : "No Data"));
                }
            }

            headersMessage += bodyMessage;

            if (originalWebException.Response != null && originalWebException.Response.Headers != null)
            {
                headersMessage += originalWebException.Response.Headers;
            }

            var wException = new WebException(originalWebException.Message + headersMessage, originalWebException);
            wException.Data.Add("jsonException", error);

            return wException;
        }
    }
}