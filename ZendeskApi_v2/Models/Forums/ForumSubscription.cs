// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Forums
{

    public class ForumSubscription
    {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("forum_id")]
        public long? ForumId { get; set; }

        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
