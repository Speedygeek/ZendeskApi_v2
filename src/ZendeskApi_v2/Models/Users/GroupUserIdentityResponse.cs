// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Users
{

    public class GroupUserIdentityResponse : GroupResponseBase
    {

        [JsonProperty("identities")]
        public IList<UserIdentity> Identities { get; set; }
    }
}
