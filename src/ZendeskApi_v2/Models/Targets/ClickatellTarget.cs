using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class ClickatellTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "clickatell_target";
            }
        }

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

        [JsonProperty("api_id")]
        public string ApiId { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("us_small_business_account", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool USSmallBusinessAccount { get; set; }
    }
}
