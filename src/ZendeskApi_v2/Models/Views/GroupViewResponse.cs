﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{
    public class GroupViewResponse : GroupResponseBase
    {
        [JsonProperty("views")]
        public IList<View> Views { get; set; }
    }
}