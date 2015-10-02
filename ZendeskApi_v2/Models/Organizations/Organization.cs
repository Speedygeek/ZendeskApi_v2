// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Organizations
{
    public interface IOrganization
    {
        string Url { get; set; }
        long? Id { get; set; }
        object ExternalId { get; set; }
        string Name { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        IList<object> DomainNames { get; set; }
        object Details { get; set; }
        object Notes { get; set; }
        object GroupId { get; set; }
        bool SharedTickets { get; set; }
        bool SharedComments { get; set; }
        IList<string> Tags { get; set; }
        IDictionary<string, object> OrganizationFields { get; set; }
    }

    public class Organization : IOrganization
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("external_id")]
        public object ExternalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("domain_names")]
        public IList<object> DomainNames { get; set; }

        [JsonProperty("details")]
        public object Details { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("group_id")]
        public object GroupId { get; set; }

        [JsonProperty("shared_tickets")]
        public bool SharedTickets { get; set; }

        [JsonProperty("shared_comments")]
        public bool SharedComments { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("organization_fields")]
        public IDictionary<string, object> OrganizationFields { get; set; }
    }
}
