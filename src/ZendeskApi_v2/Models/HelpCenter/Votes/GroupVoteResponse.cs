// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Votes
{
    public class GroupVoteResponse : GroupResponseBase
    {
        [JsonProperty("votes")]
        public IList<Vote> Votes { get; set; }
    }
}