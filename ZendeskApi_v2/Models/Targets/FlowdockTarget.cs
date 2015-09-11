using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class FlowdockTarget : BaseTarget
    {
        [JsonProperty("api_token")]
        public string APIToken { get; set; }
    }
}
