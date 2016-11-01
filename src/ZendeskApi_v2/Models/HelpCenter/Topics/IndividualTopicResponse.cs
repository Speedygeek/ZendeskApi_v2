using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Topics
{
    public class IndividualTopicResponse
    {
        [JsonProperty("Topic")]
        public Topic Topic { get; set; }
    }
}