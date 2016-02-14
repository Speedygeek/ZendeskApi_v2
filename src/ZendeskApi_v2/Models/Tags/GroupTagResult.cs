// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Tags
{

    public class GroupTagResult
    {
        [JsonProperty("tags")]
        public IList<Tag> Tags { get; set; }
    }
}
