using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Macros
{
    public class Restriction
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}