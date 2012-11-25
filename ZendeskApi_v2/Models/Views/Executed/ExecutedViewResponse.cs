// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{

    public class ExecutedViewResponse : GroupResponseBase
    {

        [JsonProperty("rows")]
        public IList<Row> Rows { get; set; }

        [JsonProperty("columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty("view")]
        public View View { get; set; }

        [JsonProperty("users")]
        public IList<ExecutedUser> Users { get; set; }    
    }
}
