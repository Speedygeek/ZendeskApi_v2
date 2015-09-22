using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    /// <summary>
    /// HTTP Target is currently available only for beta customers.
    /// </summary>
    public class HTTPTarget : BaseTarget
    {
        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }
    }
}
