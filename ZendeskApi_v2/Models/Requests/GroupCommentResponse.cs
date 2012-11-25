using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Requests
{
    public class GroupCommentResponse : GroupResponseBase
    {
        [JsonProperty("comments")]
        public IList<Comment> Comments { get; set; }
    }
}