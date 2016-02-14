using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Triggers
{
    public class IndividualTriggerResponse
    {
        [JsonProperty("trigger")]
        public Trigger Trigger { get; set; }
    }
}