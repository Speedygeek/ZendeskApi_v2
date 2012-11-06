using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using JsonFx.Json;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;
using RestSharp;
using RestSharp.Contrib;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2
{
    public class Core
    {
        private const string XOnBehalfOfEmail = "X-On-Behalf-Of";
        private RestClient _client;
        protected string User;
        protected string Password;
        protected string ZenDeskUrl;        

        /// <summary>
        /// Constructor that uses BasicHttpAuthentication.
        /// </summary>
        /// <param name="zenDeskApiUrl"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Core(string zenDeskApiUrl, string user, string password)
        {
            User = user;
            Password = password;
            ZenDeskUrl = zenDeskApiUrl;

            _client = new RestClient(zenDeskApiUrl);
            _client.Authenticator = new HttpBasicAuthenticator(user, password);
        }

        protected T GenericGet<T>(string resource)
        {
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = resource,
                RequestFormat = DataFormat.Json
            };

            return Execute<T>(request);
        }

        protected T Execute<T>(RestRequest request)
        {            
            var response = _client.Execute(request);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);            
            return obj;
        }

        protected IRestResponse Execute(RestRequest request)
        {
            var res = _client.Execute(request);
            return res;
        }

        protected bool GenericDelete(string resource)
        {
            var request = new RestRequest
            {
                Method = Method.DELETE,
                Resource = resource,
            };

            var res = Execute(request);
            return res.StatusCode == HttpStatusCode.OK;
        }
        
        protected T GenericPost<T>(string resource, object body=null)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = resource,
                RequestFormat = DataFormat.Json
            };

            if(body != null)
                request.AddAndSerializeParam(body, ParameterType.RequestBody);

            var res = Execute<T>(request);
            return res;
        }
        
        protected T GenericPut<T>(string resource, object body=null)
        {
            var request = new RestRequest
            {
                Method = Method.PUT,
                Resource = resource,
                RequestFormat = DataFormat.Json
            };

            if (body != null)
                request.AddAndSerializeParam(body, ParameterType.RequestBody);

            var res = Execute<T>(request);
            return res;
        }

        /// <summary>
        /// When using sso use this method to generate a url that logs a user in and returns them to the returnToUrl (if specified).
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="httpsUrl"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="returnToUrl"></param>
        /// <returns></returns>
        public static string GetLoginUrl(string authToken, string httpsUrl, string name, string email,
                                         string returnToUrl = "")
        {
            string url = string.Format("{0}/access/remote/", httpsUrl);

            string timestamp = GetUnixEpoch(DateTime.Now).ToString();

            string message = name + email + authToken + timestamp;
            string hash = Md5(message);

            string result = url + "?name=" + HttpUtility.UrlEncode(name) +
                            "&email=" + HttpUtility.UrlEncode(email) +
                            "&timestamp=" + timestamp +
                            "&hash=" + hash;

            if (returnToUrl.Length > 0)
                result += "&return_to=" + returnToUrl;

            return result;
        }

        private static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                           new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }


        private static string Md5(string strChange)
        {
            //Change the syllable into UTF8 code
            byte[] pass = Encoding.UTF8.GetBytes(strChange);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(pass);
            string strPassword = ByteArrayToHexString(md5.Hash);
            return strPassword;
        }

        private static string ByteArrayToHexString(byte[] Bytes)
        {
            // important bit, you have to change the byte array to hex string or zenddesk will reject
            StringBuilder Result;
            string HexAlphabet = "0123456789abcdef";

            Result = new StringBuilder();

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int) (B >> 4)]);
                Result.Append(HexAlphabet[(int) (B & 0xF)]);
            }

            return Result.ToString();
        }

        /// <summary>
        /// Gets the Collection
        /// </summary>
        /// <returns></returns>
        public List<T> GetCollection<T>(string resource, string rootElement = "")
        {
            var request = new RestRequest
                              {
                                  Method = Method.GET,
                                  Resource = resource,
                                  RootElement = rootElement
                              };

            return Execute<List<T>>(request);
        }
    }
}
