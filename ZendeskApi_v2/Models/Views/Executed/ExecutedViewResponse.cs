// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views.Executed
{

    public class ExecutedViewResponse
    {

        [JsonProperty("rows")]
        public IList<Row> Rows { get; set; }

        [JsonProperty("columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty("view")]
        public View View { get; set; }

        [JsonProperty("users")]
        public IList<ExecutedUser> Users { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
