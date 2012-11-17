using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Triggers
{
    public class IndividualTriggerResponse
    {
        [JsonProperty("trigger")]
        public Trigger Trigger { get; set; }
    }
}