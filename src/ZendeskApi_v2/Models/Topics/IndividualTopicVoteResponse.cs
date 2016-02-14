using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Topics
{
    public class IndividualTopicVoteResponse
    {
        [JsonProperty("topic_vote")]
        public TopicVote TopicVote { get; set; }
    }
}