using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class System
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}