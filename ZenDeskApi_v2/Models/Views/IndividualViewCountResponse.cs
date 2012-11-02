using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views
{
    public class IndividualViewCountResponse
    {
        [JsonProperty("view_count")]
        public ViewCount ViewCount { get; set; }
    }
}