using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Shared
{
    public  class MetaData
    {
        [JsonProperty("custom")]
        public Custom Custom { get; set; }

        [JsonProperty("system")]
        public System System { get; set; }
    }
}