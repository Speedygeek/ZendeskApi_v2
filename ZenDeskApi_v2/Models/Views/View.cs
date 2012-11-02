// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views
{

    public class View
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("restriction")]
        public object Restriction { get; set; }

        [JsonProperty("execution")]
        public Execution Execution { get; set; }

        [JsonProperty("conditions")]
        public Conditions Conditions { get; set; }
    }
}
