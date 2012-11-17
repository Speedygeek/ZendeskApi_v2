using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Topics
{
    public class IndividualTopicSubscriptionResponse
    {
        [JsonProperty("topic_subscription")]
        public TopicSubscription TopicSubscription { get; set; }
    }
}