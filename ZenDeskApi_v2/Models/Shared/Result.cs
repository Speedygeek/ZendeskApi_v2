using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Shared
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
        public int Id { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}