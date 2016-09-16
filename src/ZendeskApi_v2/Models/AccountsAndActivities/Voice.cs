// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Voice
    {

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("maintenance")]
        public bool Maintenance { get; set; }

        [JsonProperty("logging")]
        public bool Logging { get; set; }

        [JsonProperty("outbound")]
        public bool Outbound { get; set; }
    }
}
