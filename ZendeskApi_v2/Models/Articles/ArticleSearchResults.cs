// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Articles {
    public class ArticleSearchResults
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("page_count")]
        public int PageCount { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PrevPage { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }
    }
}
