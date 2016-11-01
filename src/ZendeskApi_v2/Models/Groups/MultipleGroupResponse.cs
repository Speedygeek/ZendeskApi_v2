using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class MultipleGroupResponse : GroupResponseBase
    {
        [JsonProperty("groups")]
        public IList<Group> Groups { get; set; }
    }
}