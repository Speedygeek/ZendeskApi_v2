// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class User
    {
        [JsonProperty("tagging")]
        public bool Tagging { get; set; }
    }
}