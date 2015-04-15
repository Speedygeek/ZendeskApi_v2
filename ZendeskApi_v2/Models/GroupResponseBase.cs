// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json;
using ZendeskApi_v2.Extensions;

namespace ZendeskApi_v2.Models
{
    public class GroupResponseBase
    {        
        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public string PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page")]
        public int Page
        {
            get { return GetPageFromParameter(); }
        }

        [JsonProperty("per_page")]
        public int PageSize
        {
            get { return GetPageSizeFromParameter(); }
        }

        [JsonProperty("total_pages")]
        public int TotalPages 
        {
            get { return (int)Math.Ceiling(Count/(double)PageSize); }
        }

        private int GetPageFromParameter()
        {
            if (string.IsNullOrEmpty(PreviousPage))
                return 1;

            if (string.IsNullOrEmpty(NextPage))
                return TotalPages;

            var dict = NextPage.GetQueryStringDict();
            if (dict.ContainsKey("page"))
            {
                return int.Parse(dict["page"]) - 1;
            }

            return 0;
        }

        private int GetPageSizeFromParameter()
        {
            var page = NextPage ?? PreviousPage;
            if (page == null)
                return 100;

            var dict = page.GetQueryStringDict();
            if (dict.ContainsKey("per_page"))
            {
                return int.Parse(dict["per_page"]);
            }

            return 100;
        }
    }
}
