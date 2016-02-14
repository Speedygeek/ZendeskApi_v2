using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Users
{
    public class Photo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("thumbnails")]
        public IList<Thumbnail> Thumbnails { get; set; }
    }
}