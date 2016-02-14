// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.AccountsAndActivities;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Settings
    {

        [JsonProperty("branding")]
        public Branding Branding { get; set; }

        [JsonProperty("apps")]
        public Apps Apps { get; set; }

        [JsonProperty("tickets")]
        public Tickets Tickets { get; set; }

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("voice")]
        public Voice Voice { get; set; }

        [JsonProperty("twitter")]
        public Twitter Twitter { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("screencast")]
        public Screencast Screencast { get; set; }

        [JsonProperty("lotus")]
        public Lotus Lotus { get; set; }

        [JsonProperty("gooddata_integration")]
        public GooddataIntegration GooddataIntegration { get; set; }
    }
}
