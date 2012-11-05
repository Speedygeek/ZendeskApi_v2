// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Views.Executed
{

    public class LastComment
    {

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("author_id")]
        public long AuthorId { get; set; }
    }
}
