using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class GroupTargetResponse : GroupResponseBase
    {
        [JsonProperty("targets")]
        public IList<BaseTarget> Targets { get; set; }
    }
}