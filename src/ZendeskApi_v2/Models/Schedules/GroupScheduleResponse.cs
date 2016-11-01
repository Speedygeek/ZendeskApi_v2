using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class GroupScheduleResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("schedules")]
        public IList<Schedule> Schedules { get; set; }
    }
}