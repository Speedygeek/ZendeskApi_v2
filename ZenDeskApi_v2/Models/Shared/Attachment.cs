using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Shared
{
    public class Attachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnail Thumbnails { get; set; }
    }
}