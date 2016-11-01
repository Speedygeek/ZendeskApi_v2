using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class AgentActivity
    {
        [JsonProperty("agent_id")]
        public long AgentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("via")]
        public string Via { get; set; }

        [JsonProperty("forwarding_number")]
        public string ForwardingNumber { get; set; }

        [JsonProperty("available_time")]
        public long AvailableTime { get; set; }

        [JsonProperty("calls_accepted")]
        public long CallsAccepted { get; set; }

        [JsonProperty("calls_denied")]
        public long CallsDenied { get; set; }

        [JsonProperty("calls_missed")]
        public long CallsMissed { get; set; }

        [JsonProperty("average_talk_time")]
        public long AverageTalkTime { get; set; }
    }
}