using System;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Shared
{
    public class Data
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("recording_url")]
        public string RecordingUrl { get; set; }

        [JsonProperty("call_id")]
        public long CallId { get; set; }

        [JsonProperty("call_duration")]
        public long CallDuration { get; set; }

        [JsonProperty("answered_by_id")]
        public long? AnsweredById { get; set; }

        [JsonProperty("started_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? StartedAt { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        [JsonProperty("public")]
        public bool? Public { get; set; }

        [JsonProperty("brand_id")]
        public long BrandId { get; set; }

        [JsonProperty("via_id")]
        public long ViaId { get; set; }

        [JsonProperty("answered_by_name")]
        public string AnsweredByName { get; set; }

        [JsonProperty("transcription_status")]
        public string TranscriptionStatus { get; set; }
    }
}
