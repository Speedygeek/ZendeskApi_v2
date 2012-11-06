// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Users
{

    public class UserIdentity
    {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
