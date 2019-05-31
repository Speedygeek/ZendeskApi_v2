using System.Collections.Generic;
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

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// Whether the user segment is built-in. Built-in user segments cannot be modified
        /// </summary>
        [JsonProperty("built_in")]
        public bool BuiltIn { get; set; }
    }
}
