using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZendeskApi_v2.Models.Schedules
{
    public class WorkWeek
    {
        [JsonProperty("intervals")]
        public IList<Interval> Intervals { get; set; }
    }
}