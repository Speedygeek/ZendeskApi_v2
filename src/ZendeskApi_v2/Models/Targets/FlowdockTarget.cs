using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class FlowdockTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "flowdock_target";
            }
        }

        [JsonProperty("api_token")]
        public string APIToken { get; set; }
    }
}