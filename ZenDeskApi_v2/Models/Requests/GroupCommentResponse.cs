using System.Collections.Generic;
using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Requests
{
    public class GroupCommentResponse
    {
        [JsonProperty("comments")]
        public IList<Comment> Comments { get; set; }
    }
}