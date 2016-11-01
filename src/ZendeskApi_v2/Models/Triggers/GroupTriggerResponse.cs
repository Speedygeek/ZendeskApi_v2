// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Triggers
{
    public class GroupTriggerResponse : GroupResponseBase
    {
        [JsonProperty("triggers")]
        public IList<Trigger> Triggers { get; set; }
    }
}