using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Automations
{
    public class Conditions
    {
        [JsonProperty("all")]
        public IList<All> All { get; set; }

        [JsonProperty("any")]
        public IList<All> Any { get; set; }
    }
}
