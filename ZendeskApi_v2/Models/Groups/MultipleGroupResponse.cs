using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class MultipleGroupResponse
    {

        [JsonProperty("groups")]
        public IList<Group> Groups { get; set; }

        [JsonProperty("next_page")]
        public object NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
