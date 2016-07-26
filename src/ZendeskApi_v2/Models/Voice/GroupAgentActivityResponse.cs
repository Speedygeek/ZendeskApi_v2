using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class GroupAgentActivityResponse
    {
        [JsonProperty("agents_activity")]
        public IList<AgentActivity> AgentsActivity { get; set; }
    }
}
