using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class BaseTicket
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

    }
}
