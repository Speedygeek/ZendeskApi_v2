using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class URLTarget : BaseTarget
    {
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("attribute")]
        public string Attribute { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
