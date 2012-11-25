// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Chat
    {

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("maximum_request_count")]
        public object MaximumRequestCount { get; set; }

        [JsonProperty("welcome_message")]
        public string WelcomeMessage { get; set; }
    }
}
