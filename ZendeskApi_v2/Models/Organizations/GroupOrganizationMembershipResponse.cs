using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZendeskApi_v2.Models.Organizations
{
    public class GroupOrganizationMembershipResponse : GroupResponseBase
    {
        [JsonProperty("organization_memberships")]
        public IList<OrganizationMembership> OrganizationMemberships { get; set; }
    }
}