using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Shared
{
    public  class IndividualAuditResponse
    {
        [JsonProperty("audit")]
        public Audit Audit { get; set; }
    }
}