// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Requests
{

    public class Request
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }        

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}
