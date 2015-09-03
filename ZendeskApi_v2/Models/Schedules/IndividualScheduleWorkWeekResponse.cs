using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class IndividualScheduleWorkWeekResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("workweek")]
        public WorkWeek WorkWeek { get; set; }
    }
}