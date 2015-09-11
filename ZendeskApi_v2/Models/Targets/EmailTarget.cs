using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class EmailTarget : BaseTarget
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}
