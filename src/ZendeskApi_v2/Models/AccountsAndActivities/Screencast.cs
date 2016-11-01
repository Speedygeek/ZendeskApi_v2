// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

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