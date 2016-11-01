using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.AccessPolicies
{
    public enum ViewableBy
    {
        everybody,
        signed_in_users,
        staff
    }

    public enum ManageableBy
    {
        staff,
        managers
    }

    public class AccessPolicy
    {
        [JsonProperty("viewable_by")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ViewableBy ViewableBy { get; set; }

        [JsonProperty("manageable_by")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ManageableBy ManageableBy { get; set; }

        [JsonProperty("restricted_to_group_ids")]
        public IList<long> RestrictedToGroupIds { get; set; }

        [JsonProperty("restricted_to_organization_ids")]
        public IList<long> RestrictedToOrganizationIds { get; set; }

        [JsonProperty("required_tags")]
        public IList<string> RequiredTags { get; set; }
    }
}