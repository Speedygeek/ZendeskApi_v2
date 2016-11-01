using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Topics
{
    public class GroupTopicResponse : GroupResponseBase
    {
        [JsonProperty("Topics")]
        public IList<Topic> Topics { get; set; }
    }
}