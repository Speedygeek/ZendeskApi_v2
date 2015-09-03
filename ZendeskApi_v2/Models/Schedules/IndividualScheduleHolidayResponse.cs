using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class IndividualScheduleHolidayResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("holiday")]
        public Holiday Holiday { get; set; }
    }
}