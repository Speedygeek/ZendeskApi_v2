// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;


namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Activity
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("verb")]
        public string Verb { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user")]
        public ZendeskApi_v2.Models.Users.User User { get; set; }

        [JsonProperty("actor")]
        public ZendeskApi_v2.Models.Users.User Actor { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
