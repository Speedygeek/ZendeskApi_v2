using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class PivotalTarget : BaseTarget
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("story_type")]
        public string StoryType { get; set; }

        [JsonProperty("story_title")]
        public string StoryTitle { get; set; }

        [JsonProperty("requested_by")]
        public string RequestedBy { get; set; }

        [JsonProperty("owner_by")]
        public string OwnerBy { get; set; }

        [JsonProperty("story_labels")]
        public string StoryLabels { get; set; }
    }
}
