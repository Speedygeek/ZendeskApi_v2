// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.Views;

namespace ZendeskApi_v2.Models.Views
{

    public class GroupViewResponse : GroupResponseBase
    {
        [JsonProperty("views")]
        public IList<View> Views { get; set; }
    }
}
