// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.Shared;


namespace ZendeskApi_v2.Models.Topics
{

    public class Topic
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("topic_type")]
        public string TopicType { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("updater_id")]
        public long? UpdaterId { get; set; }

        [JsonProperty("forum_id")]
        public long? ForumId { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("highlighted")]
        public bool Highlighted { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        
        /// <summary>
        /// Used for uploading Topics only
        /// When creating and updating Topics you may attach files by passing in an array of the tokens received from uploading the files. 
        /// Use Attachments.UploadAttachment to get the token first.
        /// </summary>
        [JsonProperty("uploads")]
        public IList<string> Uploads { get; set; }

    }
}
