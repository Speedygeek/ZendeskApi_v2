using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Shared
{
    public  class Audit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("author_id")]
        public long AuthorId { get; set; }

        [JsonProperty("metadata")]
        public MetaData MetaData { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("events")]
        public IList<Event> Events { get; set; }
    }
}
