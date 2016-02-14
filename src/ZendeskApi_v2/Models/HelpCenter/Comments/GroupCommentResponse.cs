// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.HelpCenter.Comments
{

   public class GroupCommentResponse : GroupResponseBase
   {
      [JsonProperty("comments")]
      public IList<Comments.Comment> Comments { get; set; }
   }
}
