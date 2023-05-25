// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Brands
{
    public class Brand
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("brand_url")]
        public string BrandUrl { get; set; }

        [JsonProperty("has_help_center")]
        public bool? HasHelpCenter { get; set; }

        [JsonProperty("has_center_state")]
        public bool? HasCenterState { get; set; }

		[JsonProperty("help_center_state")]
        public string HelpCenterState { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("default")]
        public bool? IsDefault { get; set; }

        [JsonProperty("logo")]
        public Attachment Logo { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        [JsonProperty("host_mapping")]
        public string HostMapping { get; set; }

        [JsonProperty("signature_template")]
        public string SignatureTemplate { get; set; }
    }
}
