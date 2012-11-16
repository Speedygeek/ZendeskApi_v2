// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZenDeskApi_v2.Models.Shared;

namespace ZenDeskApi_v2.Models.Topics
{

    public class TopicComment
    {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("topic_id")]
        public long? TopicId { get; set; }

        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("informative")]
        public bool Informative { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
