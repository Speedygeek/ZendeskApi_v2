using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class TwitterTarget : BaseTarget
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
