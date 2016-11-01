using Newtonsoft.Json;
using ZendeskApi_v2.Serialization;

namespace ZendeskApi_v2.Models.Targets
{
    public class IndividualTargetResponse
    {
        [JsonProperty("target")]
        [JsonConverter(typeof(TargetJsonConverter))]
        public BaseTarget Target { get; set; }
    }
}