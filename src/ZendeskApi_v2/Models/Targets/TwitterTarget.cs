using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class TwitterTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "twitter_target";
            }
        }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
