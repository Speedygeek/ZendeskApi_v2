using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class EmailCC
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}
