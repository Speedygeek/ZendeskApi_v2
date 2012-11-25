// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{

    public class Conditions
    {

        [JsonProperty("all")]
        public IList<All> All { get; set; }

        [JsonProperty("any")]
        public IList<object> Any { get; set; }
    }
}
