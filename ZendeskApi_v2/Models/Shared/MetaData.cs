using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Shared
{
    public  class MetaData
    {
        [JsonProperty("custom")]
        public Custom Custom { get; set; }

        [JsonProperty("system")]
        public System System { get; set; }
    }
}