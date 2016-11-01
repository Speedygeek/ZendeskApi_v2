// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Serialization;

namespace ZendeskApi_v2.Models.Macros
{
    public class Action
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public IList<string> Value { get; set; }
    }
}