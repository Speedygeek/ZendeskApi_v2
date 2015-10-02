// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Search
{
    public interface ISearchResultsT<T>
    {
        int Count { get; set; }
        string NextPage { get; set; }
        object PrevPage { get; set; }
        IList<T> Results { get; set; }
        object Error { get; set; }
        object Description { get; set; }
    }

    public class SearchResults<T> : ISearchResultsT<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("prev_page")]
        public object PrevPage { get; set; }

        [JsonProperty("results")]
        public IList<T> Results { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }

    public class SearchResults : ISearchResultsT<IResult>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("prev_page")]
        public object PrevPage { get; set; }

        [JsonProperty("results")]
        public IList<IResult> Results { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }
}
