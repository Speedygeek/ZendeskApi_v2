using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZendeskApi_v2.Models.HelpCenter.Post
{
    public class GroupPostResponse : GroupResponseBase
    {
        [JsonProperty("Posts")]
        public IList<Post> Posts { get; set; }
    }
}
