using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace ZendeskApi_v2.Models.Voice
{
    public class CurrentQueueActivity
    {
        [JsonProperty("agents_online")]
        public long AgentsOnline { get; set; }

        [JsonProperty("calls_waiting")]
        public long CallsWaiting { get; set; }

        [JsonProperty("callbacks_waiting")]
        public long CallbacksWaiting { get; set; }

        [JsonProperty("average_wait_time")]
        public long AverageWaitTime { get; set; }

        [JsonProperty("longest_wait_time")]
        public long LongestWaitTime { get; set; }
    }
}
