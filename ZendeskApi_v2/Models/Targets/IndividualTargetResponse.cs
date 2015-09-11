using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class IndividualTargetResponse
    {
        [JsonProperty("target")]
        public BaseTarget Target { get; set; }
    }
}
