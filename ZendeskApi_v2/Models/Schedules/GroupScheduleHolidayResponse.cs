using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZendeskApi_v2.Models.Schedules
{
    public class GroupScheduleHolidayResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("holidays")]
        public IList<Holiday> Holidays { get; set; }
    }
}