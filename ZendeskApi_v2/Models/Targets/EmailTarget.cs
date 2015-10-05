using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class EmailTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "email_target";
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}
