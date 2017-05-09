using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TimeSpanMetric
    {

        [JsonProperty("calendar")]
        public long? Calendar { get; set; }

        [JsonProperty("business")]
        public long? Business { get; set; }
    }
}
