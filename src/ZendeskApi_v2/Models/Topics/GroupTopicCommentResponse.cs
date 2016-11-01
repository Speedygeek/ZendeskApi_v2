// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Topics
{
    public class GroupTopicCommentResponse : GroupResponseBase
    {
        [JsonProperty("topic_comments")]
        public IList<TopicComment> TopicComments { get; set; }
    }
}