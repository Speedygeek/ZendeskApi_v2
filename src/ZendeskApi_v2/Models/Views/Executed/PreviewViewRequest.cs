using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{
    public class PreviewViewRequest
    {
        [JsonProperty("view")]
        public PreviewView View { get; set; }

        [JsonProperty("group_by")]
        public string GroupBy { get; set; }

        [JsonProperty("group_order")]
        public string GroupOrder { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }
    }
}