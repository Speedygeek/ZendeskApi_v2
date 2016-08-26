// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Organizations
{

    public class Organization : IndividualSearchableResponseBase
    {
        [JsonProperty("external_id")]
        public object ExternalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("domain_names")]
        public IList<object> DomainNames { get; set; }

        [JsonProperty("details")]
        public object Details { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("group_id")]
        public object GroupId { get; set; }

        [JsonProperty("shared_tickets", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool SharedTickets { get; set; }

        [JsonProperty("shared_comments", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool SharedComments { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("organization_fields")]
        public IDictionary<string, object> OrganizationFields { get; set; }
    }
}
