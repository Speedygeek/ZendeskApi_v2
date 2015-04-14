// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.HelpCenter.Votes
{

   public class GroupVoteResponse : GroupResponseBase
   {

      [JsonProperty("votes")]
      public IList<Vote> Votes { get; set; }
   }
}
