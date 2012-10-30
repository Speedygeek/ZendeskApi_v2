using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Shared
{
    public class Via
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
}
