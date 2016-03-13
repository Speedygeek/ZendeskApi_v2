using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using ZendeskApi_v2.Serialization;

namespace ZendeskApi_v2.Models.Schedules
{
    public class Holiday
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("end_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? EndDate { get; set; }
    }
}