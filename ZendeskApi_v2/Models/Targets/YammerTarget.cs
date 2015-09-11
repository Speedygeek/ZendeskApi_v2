using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class YammerTarget : BaseTarget
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
