using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class Options
    {

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("hour_offset")]
        public string HourOffset { get; set; }
    }
}