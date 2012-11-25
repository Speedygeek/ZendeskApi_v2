using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class GroupUserResponse
    {

        [JsonProperty("users")]
        public IList<User> Users { get; set; }

        [JsonProperty("next_page")]
        public object NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}