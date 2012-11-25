using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class GroupAuditResponse
    {
        [JsonProperty("audits")]
        public IList<Audit> Audits { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}