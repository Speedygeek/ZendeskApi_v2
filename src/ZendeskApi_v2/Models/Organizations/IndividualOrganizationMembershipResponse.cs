using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Organizations
{
    public class IndividualOrganizationMembershipResponse
    {
        [JsonProperty("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}