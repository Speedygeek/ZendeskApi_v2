// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Extensions;

namespace ZendeskApi_v2.Models
{
    public interface IGroupResponseBase
    {
        string NextPage { get; set; }
        string PreviousPage { get; set; }
        long Count { get; set; }
        long Page { get; }
        long PageSize { get; }
        long TotalPages { get; }
    }

    public class GroupResponseBase:IGroupResponseBase
    {
        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public string PreviousPage { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("page")]
        public long Page
        {
            get { return GetPageFromParameter(); }
        }

        [JsonProperty("per_page")]
        public long PageSize
        {
            get { return GetPageSizeFromParameter(); }
        }

        [JsonProperty("total_pages")]
        public long TotalPages
        {
            get { return (int)Math.Ceiling(Count / (double)PageSize); }
        }

        private long GetPageFromParameter()
        {
            if (string.IsNullOrEmpty(PreviousPage))
            {
                return 1;
            }

            if (string.IsNullOrEmpty(NextPage))
            {
                return TotalPages;
            }

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
            {
                return 100;
            }

            var dict = page.GetQueryStringDict();
            if (dict.ContainsKey("per_page"))
            {
                return int.Parse(dict["per_page"]);
            }

            return 100;
        }
    }
}
