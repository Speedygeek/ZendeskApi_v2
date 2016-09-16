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

        [JsonProperty("list_newest_comments_first")]
        public bool ListNewestCommentsFirst { get; set; }

        [JsonProperty("collaboration")]
        public bool Collaboration { get; set; }

        [JsonProperty("private_attachments")]
        public bool PrivateAttachments { get; set; }

        [JsonProperty("agent_collision")]
        public bool AgentCollision { get; set; }

        [JsonProperty("tagging")]
        public bool Tagging { get; set; }

        [JsonProperty("list_empty_views")]
        public bool ListEmptyViews { get; set; }

        [JsonProperty("comments_public_by_default")]
        public bool CommentsPublicByDefault { get; set; }

        [JsonProperty("maximum_personal_views_to_list")]
        public int MaximumPersonalViewsToList { get; set; }

        [JsonProperty("status_hold")]
        public bool StatusHold { get; set; }
    }
}
