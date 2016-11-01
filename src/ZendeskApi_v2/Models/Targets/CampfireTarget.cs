using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class CampfireTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "campfire_target";
            }
        }

        [JsonProperty("subdomain")]
        public string SubDomain { get; set; }

        [JsonProperty("ssl")]
        public bool SSL { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("preserve_format")]
        public bool PreserveFormat { get; set; }
    }
}