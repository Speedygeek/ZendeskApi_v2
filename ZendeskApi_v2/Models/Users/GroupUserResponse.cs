using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class GroupUserResponse : GroupResponseBase
    {

        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}