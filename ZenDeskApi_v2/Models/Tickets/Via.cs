using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class Via
    {

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
}
