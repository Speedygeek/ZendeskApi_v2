using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class Result
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
    }
}
