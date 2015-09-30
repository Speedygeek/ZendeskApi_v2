using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Models.Requests
{
    public class GroupCommentResponse : GroupResponseBase
    {
        [JsonProperty("comments")]
        public IList<Comment> Comments { get; set; }

        [JsonProperty("users")]
        public IList<User> Users { get; set; }

        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }
    }
}