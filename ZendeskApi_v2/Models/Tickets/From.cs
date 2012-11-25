using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{

    public class From
    {

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
