using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class MultipleGroupMembershipResponse
    {

        [JsonProperty("group_memberships")]
        public IList<GroupMembership> GroupMemberships { get; set; }

        [JsonProperty("next_page")]
        public object NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}