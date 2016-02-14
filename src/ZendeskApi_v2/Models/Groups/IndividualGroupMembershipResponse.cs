using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class IndividualGroupMembershipResponse
    {
        [JsonProperty("group_membership")]
        public GroupMembership GroupMembership { get; set; }        
    }
}