using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{
    public class IndividualViewCountResponse
    {
        [JsonProperty("view_count")]
        public ViewCount ViewCount { get; set; }
    }
}