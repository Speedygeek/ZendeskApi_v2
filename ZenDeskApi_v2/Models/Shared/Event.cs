using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Shared
{
    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }
    }
}