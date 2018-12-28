// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class GroupUserIdentityResponse : GroupResponseBase
    {
        [JsonProperty("identities")]
        public IList<UserIdentity> Identities { get; set; }
    }
}
