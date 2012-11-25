using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{
    public class IndividualViewResponse
    {
        [JsonProperty("view")]
        public View View { get; set; }
    }
}