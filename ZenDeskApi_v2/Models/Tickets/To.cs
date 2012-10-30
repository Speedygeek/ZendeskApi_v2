using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{

    public class To
    {

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
