using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class WorkWeek
    {
        [JsonProperty("intervals")]
        public IList<Interval> Intervals { get; set; }
    }
}