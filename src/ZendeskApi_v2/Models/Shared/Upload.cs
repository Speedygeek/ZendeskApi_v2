using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class Upload
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }
    }
}