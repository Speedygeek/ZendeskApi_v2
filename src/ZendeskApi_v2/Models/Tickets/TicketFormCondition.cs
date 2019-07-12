using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TicketFormCondition
    {
        [JsonProperty("parent_field_id")]
        public long? ParentFieldId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("child_fields")]
        public IList<ChildField> ChildFields { get; set; }
    }
}
