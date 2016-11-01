// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class GroupActivityResponse : GroupResponseBase
    {
        [JsonProperty("activities")]
        public IList<Activity> Activities { get; set; }
    }
}