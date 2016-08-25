// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.CustomRoles
{

    public class Configuration
    {

        [JsonProperty("chat_access", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ChatAccess { get; set; }

        [JsonProperty("end_user_profile")]
        public string EndUserProfile { get; set; }

        [JsonProperty("forum_access")]
        public string ForumAccess { get; set; }

        [JsonProperty("forum_access_restricted_content", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ForumAccessRestrictedContent { get; set; }

        [JsonProperty("macro_access")]
        public string MacroAccess { get; set; }

        [JsonProperty("manage_business_rules", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ManageBusinessRules { get; set; }

        [JsonProperty("manage_dynamic_content", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ManageDynamicContent { get; set; }

        [JsonProperty("manage_extensions_and_channels", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ManageExtensionsAndChannels { get; set; }

        [JsonProperty("manage_facebook", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ManageFacebook { get; set; }

        [JsonProperty("organization_editing", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool OrganizationEditing { get; set; }

        [JsonProperty("report_access")]
        public string ReportAccess { get; set; }

        [JsonProperty("ticket_access")]
        public string TicketAccess { get; set; }

        [JsonProperty("ticket_comment_access")]
        public string TicketCommentAccess { get; set; }

        [JsonProperty("ticket_deletion", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TicketDeletion { get; set; }

        [JsonProperty("ticket_editing", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TicketEditing { get; set; }

        [JsonProperty("ticket_merge", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TicketMerge { get; set; }

        [JsonProperty("ticket_tag_editing", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TicketTagEditing { get; set; }

        [JsonProperty("twitter_search_access", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool TwitterSearchAccess { get; set; }

        [JsonProperty("view_access")]
        public string ViewAccess { get; set; }
    }
}
