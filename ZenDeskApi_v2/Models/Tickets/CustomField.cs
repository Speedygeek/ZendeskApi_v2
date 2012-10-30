using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{

    public class CustomField
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
