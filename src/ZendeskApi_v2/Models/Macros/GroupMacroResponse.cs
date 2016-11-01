// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Macros
{
    public class GroupMacroResponse : GroupResponseBase
    {
        [JsonProperty("macros")]
        public IList<Macro> Macros { get; set; }
    }
}