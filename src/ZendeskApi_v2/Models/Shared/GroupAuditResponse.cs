using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class GroupAuditResponse : GroupResponseBase
    {
        [JsonProperty("audits")]
        public IList<Audit> Audits { get; set; }
    }
}