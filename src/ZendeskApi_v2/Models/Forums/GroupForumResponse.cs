// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Forums
{
    public class GroupForumResponse : GroupResponseBase
    {
        [JsonProperty("forums")]
        public IList<Forum> Forums { get; set; }
    }
}