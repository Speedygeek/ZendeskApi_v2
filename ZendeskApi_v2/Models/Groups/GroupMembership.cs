using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class GroupMembership
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}