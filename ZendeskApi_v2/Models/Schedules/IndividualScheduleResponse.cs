using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class IndividualScheduleResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("schedule")]
        public Schedule Schedule { get; set; }
    }
}