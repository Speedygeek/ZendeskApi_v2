// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class Chat
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("maximum_request_count")]
        public object MaximumRequestCount { get; set; }

        [JsonProperty("welcome_message")]
        public string WelcomeMessage { get; set; }
    }
}