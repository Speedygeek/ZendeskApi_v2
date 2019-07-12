using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Tickets
{
    public class ChildField
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("is_required")]
        public bool IsRequired { get; set; }
    }
}
