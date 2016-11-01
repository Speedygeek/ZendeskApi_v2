// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Comments
{
    public class GroupCommentResponse : GroupResponseBase
    {
        [JsonProperty("comments")]
        public IList<Comments.Comment> Comments { get; set; }
    }
}