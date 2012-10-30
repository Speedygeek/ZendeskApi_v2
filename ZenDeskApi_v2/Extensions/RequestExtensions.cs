using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace ZenDeskApi_v2.Extensions
{
    public static class RequestExtensions
    {
        public static void AddAndSerializeParam(this RestRequest request, object obj, ParameterType parameterType)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", json, parameterType);
        }
    }
}
