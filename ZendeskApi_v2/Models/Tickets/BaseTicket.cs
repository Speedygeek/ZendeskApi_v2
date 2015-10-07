using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Tickets
{
    public interface IBaseTicket
    {
        string Url { get; set; }
        long? Id { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }

    public class BaseTicket : IBaseTicket
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

    }
}
