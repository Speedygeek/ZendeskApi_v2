using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace ZenDeskApi_v2.Extensions
{
    public static class RequestExtensions
    {
        public static void AddAndSerializeParam(this RestRequest request, object obj, ParameterType parameterType)
        {                  
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
            request.AddParameter("application/json", json, parameterType);
        }

        public static string ToCsv(this List<int> ids )
        {
            return string.Join(",", ids.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray());
        }

        public static T ConvertToObject<T>(this string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
