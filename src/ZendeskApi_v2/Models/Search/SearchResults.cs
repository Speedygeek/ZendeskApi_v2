// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Search
{

    public class SearchResults<T> : GroupResponseBase
    {
        [JsonProperty("prev_page")]
        public string PrevPage { get; set; }

        [Obsolete("This is not used in SearchResults. Please use PrevPage.", true)]
        public new string PreviousPage { get; set; }

        [JsonProperty("results")]
        public IList<T> Results { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }

    public class SearchResults : GroupResponseBase
    {
        [JsonProperty("prev_page")]
        public string PrevPage { get; set; }

        [Obsolete("This is not used in SearchResults. Please use PrevPage.", true)]
        public new string PreviousPage { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }
}
