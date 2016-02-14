using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class JobStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("progress")]
        public int? Progress { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }
    }
}