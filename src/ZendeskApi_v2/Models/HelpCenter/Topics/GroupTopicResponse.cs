using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZendeskApi_v2.Models.HelpCenter.Topics
{
    public class GroupTopicResponse : GroupResponseBase
    {
        [JsonProperty("Topics")]
        public IList<Topic> Topics { get; set; }
    }
}
