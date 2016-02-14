// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Branding
    {

        [JsonProperty("header_color")]
        public string HeaderColor { get; set; }

        [JsonProperty("page_background_color")]
        public string PageBackgroundColor { get; set; }

        [JsonProperty("tab_background_color")]
        public string TabBackgroundColor { get; set; }

        [JsonProperty("text_color")]
        public string TextColor { get; set; }
    }
}
