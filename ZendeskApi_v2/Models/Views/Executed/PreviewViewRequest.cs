using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{
    public class PreviewViewRequest
    {
        [JsonProperty("view")]
        public PreviewView View { get; set; }

        [JsonProperty("group_by")]
        public string GroupBy { get; set; }

        [JsonProperty("group_order")]
        public string GroupOrder { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }
    }

    public class PreviewView
    {

        [JsonProperty("all")]
        public IList<All> All { get; set; }

        [JsonProperty("output")]
        public PreviewViewOutput Output { get; set; }
    }

    public class PreviewViewOutput
    {

        [JsonProperty("columns")]
        public IList<string> Columns { get; set; }
    }
}