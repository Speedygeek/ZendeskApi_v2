// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Triggers
{

    public class Conditions
    {

        [JsonProperty("all")]
        public IList<All> All { get; set; }

        [JsonProperty("any")]
        public IList<All> Any { get; set; }        
    }
}
