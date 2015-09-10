using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class Interval
    {
        [JsonProperty("start_time")]
        public int? StartTime { get; set; }

        [JsonProperty("end_time")]
        public int? EndTime { get; set; }
    }
}