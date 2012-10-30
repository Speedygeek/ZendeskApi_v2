using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class Comment
    {
        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}