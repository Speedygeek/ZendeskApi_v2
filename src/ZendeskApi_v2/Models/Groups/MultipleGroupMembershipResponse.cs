using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class MultipleGroupMembershipResponse : GroupResponseBase
    {

        [JsonProperty("group_memberships")]
        public IList<GroupMembership> GroupMemberships { get; set; }    
    }
}