using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class JiraTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "jira_target";
            }
        }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
