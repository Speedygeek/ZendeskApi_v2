using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Topics
{
    public class IndividualTopicVoteResponse
    {
        [JsonProperty("topic_vote")]
        public TopicVote TopicVote { get; set; }
    }
}