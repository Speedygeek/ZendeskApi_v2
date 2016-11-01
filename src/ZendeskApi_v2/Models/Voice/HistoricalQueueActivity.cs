using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class HistoricalQueueActivity
    {
        [JsonProperty("historical_queue_activity")]
        public HistoricalQueueActivityDetails Details { get; set; }
    }
}