// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Search
{

    public class SearchResults
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("prev_page")]
        public object PrevPage { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }
}
