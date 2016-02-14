// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Forums
{

    public class GroupForumSubcriptionResponse : GroupResponseBase
    {

        [JsonProperty("forum_subscriptions")]
        public IList<ForumSubscription> ForumSubscriptions { get; set; }
    }
}
