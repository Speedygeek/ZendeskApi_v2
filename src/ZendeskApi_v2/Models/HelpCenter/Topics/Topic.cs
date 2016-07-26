using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Topics
{
    public class Topic : HelpCenterBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("position")]
        public long? Position { get; set; }

        [JsonProperty("follower_count")]
        public long? FollowerCount { get; set; }
    }
}
