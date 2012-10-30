using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{

    public class Field
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
