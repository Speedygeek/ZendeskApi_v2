using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Shared
{
    public class Attachment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("article_id")]
        public long ArticleId { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        public string MappedContentUrl { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("inline")]
        public bool Inline { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("thumbnails")]
        public IList<Thumbnail> Thumbnails { get; set; }
    }
}