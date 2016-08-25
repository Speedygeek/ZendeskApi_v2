// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Tickets
    {

        [JsonProperty("list_newest_comments_first", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ListNewestCommentsFirst { get; set; }

        [JsonProperty("collaboration", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Collaboration { get; set; }

        [JsonProperty("private_attachments", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool PrivateAttachments { get; set; }

        [JsonProperty("agent_collision", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool AgentCollision { get; set; }

        [JsonProperty("tagging", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Tagging { get; set; }

        [JsonProperty("list_empty_views", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ListEmptyViews { get; set; }

        [JsonProperty("comments_public_by_default", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool CommentsPublicByDefault { get; set; }

        [JsonProperty("maximum_personal_views_to_list")]
        public int MaximumPersonalViewsToList { get; set; }

        [JsonProperty("status_hold", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool StatusHold { get; set; }
    }
}
