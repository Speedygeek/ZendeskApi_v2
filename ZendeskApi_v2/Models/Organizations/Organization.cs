// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Organizations
{

    public class Organization
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
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

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
        public IList<object> Tags { get; set; }

        [JsonProperty("organization_fields")]
        public IDictionary<string, string> OrganizationFields { get; set; }
    }
}
