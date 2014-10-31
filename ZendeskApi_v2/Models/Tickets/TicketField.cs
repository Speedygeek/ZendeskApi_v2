using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Tickets
{

    /// <summary>
    /// This class is used when creating a ticket
    /// </summary>
    public class TicketFieldBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("collapsed_for_agents")]
        public bool CollapsedForAgents { get; set; }

        [JsonProperty("regexp_for_validation")]
        public object RegexpForValidation { get; set; }

        [JsonProperty("title_in_portal")]
        public string TitleInPortal { get; set; }

        [JsonProperty("visible_in_portal")]
        public bool VisibleInPortal { get; set; }

        [JsonProperty("editable_in_portal")]
        public bool EditableInPortal { get; set; }

        [JsonProperty("required_in_portal")]
        public bool RequiredInPortal { get; set; }

        [JsonProperty("tag")]
        public object Tag { get; set; }

        [JsonProperty("custom_field_options")]
        public IList<CustomFieldOptions> CustomFieldOptions { get; set; }

    }

    public class TicketField : TicketFieldBase
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public  class CustomFieldOptions
    {
        [JsonProperty("id")]
        public long? Id{ get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
