using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views
{
    public class ViewCount
    {

        [JsonProperty("view_id")]
        public int ViewId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }

        [JsonProperty("fresh")]
        public bool Fresh { get; set; }
    }
}