﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Search
{
    
    public class Result
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("topic_type")]
        public string TopicType { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("updater_id")]
        public long? UpdaterId { get; set; }

        [JsonProperty("forum_id")]
        public long? ForumId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        [JsonProperty("comments_count")]
        public long? CommentsCount { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Can be: ticket, user, group, organization, or topic
        /// </summary>
        [JsonProperty("result_type")]
        public string ResultType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
