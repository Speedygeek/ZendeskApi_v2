using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Automations
{
    public class All
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
