// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Topics
{

    public class GroupTopicResponse : GroupResponseBase
    {

        [JsonProperty("topics")]
        public IList<Topic> Topics { get; set; }    
    }
}
