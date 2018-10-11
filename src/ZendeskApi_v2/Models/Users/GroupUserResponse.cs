using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.CustomRoles;
using ZendeskApi_v2.Models.Organizations;

namespace ZendeskApi_v2.Models.Users
{
    public class GroupUserResponse : GroupResponseBase
    {

        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }

        [JsonProperty("roles")]
        public IList<CustomRole> Roles { get; set; }

        [JsonProperty("identities")]
        public IList<UserIdentity> Identities { get; set; }

        [JsonProperty("groups")]
        public IList<Groups.Group> Groups { get; set; }

        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}