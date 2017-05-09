using System;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Shared
{
    public class Data
    {
        [JsonProperty("from")]
        public string from { get; set; }

        [JsonProperty("to")]
        public string to { get; set; }

        [JsonProperty("recording_url")]
        public string recording_url { get; set; }

        [JsonProperty("call_id")]
        public long call_id { get; set; }

        [JsonProperty("call_duration")]
        public long call_duration { get; set; }

        [JsonProperty("answered_by_id")]
        public long? answered_by_id { get; set; }

        [JsonProperty("started_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? started_at { get; set; }

        [JsonProperty("")]
        public string MyProperty { get; set; }

        [JsonProperty("location")]
        public string location { get; set; }

        [JsonProperty("author_id")]
        public long? author_id { get; set; }

        [JsonProperty("public")]
        public bool? Public { get; set; }

        [JsonProperty("brand_id")]
        public long brand_id { get; set; }

        [JsonProperty("via_id")]
        public long via_id { get; set; }

        [JsonProperty("answered_by_name")]
        public string answered_by_name { get; set; }

        [JsonProperty("transcription_status")]
        public string transcription_status { get; set; }
    }
}
