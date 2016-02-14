using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TimeSpanMetric
    {

        [JsonProperty("calendar")]
        public int? Calendar { get; set; }

        [JsonProperty("business")]
        public int? Business { get; set; }
    }
}
