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
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] 
        [JsonProperty("user_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public UserType UserType { get; set; }

        [JsonProperty("group_ids")]
        public IList<int> GroupIds { get; set; }

        [JsonProperty("organization_ids")]
        public IList<int> OrganizationIds { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }
    }
}
