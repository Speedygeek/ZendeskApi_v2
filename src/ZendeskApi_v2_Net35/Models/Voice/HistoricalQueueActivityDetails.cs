﻿using System;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{

    public class HistoricalQueueActivityDetails
    {
        [JsonProperty("total_calls")]
        public long TotalCalls { get; set; }

        [JsonProperty("most_calls_waiting")]
        public long MostCallsWaiting { get; set; }

        [JsonProperty("average_wait_time")]
        public long AverageWaitTime { get; set; }

        [JsonProperty("longest_wait_time")]
        public long LongestWaitTime { get; set; }

        [JsonProperty("average_talk_time")]
        public long AverageTalkTime { get; set; }

        [JsonProperty("last_updated_at")]
        public DateTimeOffset? LastUpdatedAt { get; set; }
    }
}