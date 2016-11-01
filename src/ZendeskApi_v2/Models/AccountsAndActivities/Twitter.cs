// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class Twitter
    {
        [JsonProperty("shorten_url")]
        public string ShortenUrl { get; set; }
    }
}