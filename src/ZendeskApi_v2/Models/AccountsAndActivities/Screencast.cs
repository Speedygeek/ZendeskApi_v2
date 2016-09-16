// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Screencast
    {

        [JsonProperty("enabled_for_tickets")]
        public bool EnabledForTickets { get; set; }

        [JsonProperty("host")]
        public object Host { get; set; }

        [JsonProperty("tickets_recorder_id")]
        public object TicketsRecorderId { get; set; }
    }
}
