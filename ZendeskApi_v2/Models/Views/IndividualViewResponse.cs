using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views
{
    public class IndividualViewResponse
    {
        [JsonProperty("view")]
        public View View { get; set; }
    }
}