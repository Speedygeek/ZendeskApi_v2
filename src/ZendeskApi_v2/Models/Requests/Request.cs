// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
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
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        /// <summary>
        /// If true, end user can mark request as solved.
        /// This will be ignored when updating a request.
        /// Consider this as readonly.
        /// </summary>
        [JsonProperty("can_be_solved_by_me", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool? CanBeSolvedByMe { get; set; }

        /// <summary>
        /// Whether or not request is solved (an end user can set this if "CanBeSolvedByMe" is true).
        /// This will be ignored when updating a request if "CanBeSolvedByMe" is false.
        /// </summary>
        [JsonProperty("solved", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool? Solved { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}
