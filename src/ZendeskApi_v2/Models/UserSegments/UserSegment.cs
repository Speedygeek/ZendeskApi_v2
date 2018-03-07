using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.UserSegments
{
    public enum UserType
    {
        signed_in_users,
        staff
    }

    public class UserSegment
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] 
        [JsonProperty("user_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public UserType UserType { get; set; }

        [JsonProperty("group_ids")]
        public IList<long> GroupIds { get; set; }

        [JsonProperty("organization_ids")]
        public IList<long> OrganizationIds { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }
    }
}
