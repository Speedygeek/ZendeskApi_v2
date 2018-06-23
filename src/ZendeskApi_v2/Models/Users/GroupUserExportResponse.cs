using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Organizations;

namespace ZendeskApi_v2.Models.Users
{
    public class GroupUserExportResponse
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

        [JsonProperty("identities")]
        public IList<UserIdentity> Identities { get; set; }

        [JsonProperty("groups")]
        public IList<Groups.Group> Groups { get; set; }

        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}
