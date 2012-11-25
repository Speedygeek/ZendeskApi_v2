using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Topics
{
    public class IndividualTopicResponse
    {
        [JsonProperty("topic")]
        public Topic Topic { get; set; }
    }
}