using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{

    public class PreviewView
    {
        [JsonProperty("all")]
        public IList<All> All { get; set; }

        [JsonProperty("output")]
        public PreviewViewOutput Output { get; set; }
    }
}