// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Triggers
{

    public class Action
    {

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
