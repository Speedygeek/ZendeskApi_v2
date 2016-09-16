using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{
    public class ViewCount
    {

        [JsonProperty("view_id")]
        public long ViewId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("value")]
        public long? Value { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }

        [JsonProperty("fresh")]
        public bool Fresh { get; set; }
    }
}