using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TicketForm
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("end_user_visible", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool EndUserVisible { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("ticket_field_ids")]
        public IList<long> TicketFieldIds { get; set; }

        [JsonProperty("active", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Active { get; set; }

        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Default { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
