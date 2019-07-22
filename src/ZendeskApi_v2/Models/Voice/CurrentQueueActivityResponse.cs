using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class CurrentQueueActivityResponse
    {
        [JsonProperty("current_queue_activity")]
        public CurrentQueueActivity CurrentQueueActivity { get; set; }
    }
}
