using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class Custom
    {
        [JsonProperty("time_spent")]
        public string TimeSpent { get; set; }
    }
}