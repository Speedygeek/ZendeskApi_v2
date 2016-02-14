// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Views
{

    public class Execution
    {

        [JsonProperty("group_by")]
        public string GroupBy { get; set; }

        [JsonProperty("group_order")]
        public string GroupOrder { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; }

        [JsonProperty("custom_fields")]
        public IList<object> CustomFields { get; set; }
    }
}
