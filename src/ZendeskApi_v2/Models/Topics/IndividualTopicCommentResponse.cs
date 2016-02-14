using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Topics
{
    public class IndividualTopicCommentResponse
    {
        [JsonProperty("topic_comment")]
        public TopicComment TopicComment { get; set; }
    }
}