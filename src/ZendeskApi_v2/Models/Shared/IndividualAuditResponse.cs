using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public  class IndividualAuditResponse
    {
        [JsonProperty("audit")]
        public Audit Audit { get; set; }
    }
}