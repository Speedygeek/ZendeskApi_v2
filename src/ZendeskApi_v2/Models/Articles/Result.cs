// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Articles
{
    public class Result
    { 
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        [JsonProperty("comments_disabled")]
        public bool CommentsDisabled { get; set; }

        [JsonProperty("draft")]
        public bool Draft { get; set; }

        [JsonProperty("promoted")]
        public bool Promoted { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("vote_sum")]
        public long VoteSum { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("section_id")]
        public long SectionId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("source_locale")]
        public string SourceLocale { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("outdated")]
        public bool Outdated { get; set; }

        [JsonProperty("result_type")]
        public string ResultType { get; set; }
    }
}
