// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Triggers
{

    public class GroupTriggerResponse : GroupResponseBase
    {

        [JsonProperty("triggers")]
        public IList<Trigger> Triggers { get; set; }
    }
}
