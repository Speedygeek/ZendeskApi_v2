using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Organizations
{
    public class GroupOrganizationExportResponse
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("end_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset EndTime { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }
    }
}
