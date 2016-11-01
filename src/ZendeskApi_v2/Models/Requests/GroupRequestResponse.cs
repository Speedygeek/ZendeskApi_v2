// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Requests
{
    public class GroupRequestResponse : GroupResponseBase
    {
        [JsonProperty("requests")]
        public IList<Request> Requests { get; set; }
    }
}