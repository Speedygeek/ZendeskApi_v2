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

        [JsonProperty("enabled", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Enabled { get; set; }

        [JsonProperty("maintenance", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Maintenance { get; set; }

        [JsonProperty("logging", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Logging { get; set; }

        [JsonProperty("outbound", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Outbound { get; set; }
    }
}
