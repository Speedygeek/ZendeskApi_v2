using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Topics
{
    public class IndividualTopicResponse
    {
        [JsonProperty("topic")]
        public Topic Topic { get; set; }
    }
}