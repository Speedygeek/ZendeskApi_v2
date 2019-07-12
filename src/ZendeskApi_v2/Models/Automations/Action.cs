using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Automations
{
    public class Action
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
