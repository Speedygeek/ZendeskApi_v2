using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{

    public class PreviewViewOutput
    {
        [JsonProperty("columns")]
        public IList<string> Columns { get; set; }
    }
}