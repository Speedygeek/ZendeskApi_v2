// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Forums
{

    public class GroupForumResponse
    {
        [JsonProperty("forums")]
        public IList<Forum> Forums { get; set; }
    }
}
