using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class AccountOverview
    {
        [JsonProperty("average_call_duration")]
        public long AverageCallDuration { get; set; }

        [JsonProperty("average_queue_wait_time")]
        public long AverageQueueWaitTime { get; set; }

        [JsonProperty("average_wrap_up_time")]
        public long AverageWarpUpTime { get; set; }

        [JsonProperty("max_calls_waiting")]
        public long MaxCallsWaiting { get; set; }

        [JsonProperty("max_queue_wait_time")]
        public long MaxQueueWaitTime { get; set; }

        [JsonProperty("total_call_duration")]
        public long TotalCallDuration { get; set; }

        [JsonProperty("total_calls")]
        public long TotalCalls { get; set; }

        [JsonProperty("total_voicemails")]
        public long TotalVoicemails { get; set; }

        [JsonProperty("total_wrap_up_time")]
        public long TotalWrapUpTime { get; set; }
    }
}
