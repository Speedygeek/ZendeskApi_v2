using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class GroupAgentActivityResponse : GroupResponseBase
    {
        [JsonProperty("agents_activity")]
        public IList<AgentActivity> AgentActivity { get; set; }
    }
}