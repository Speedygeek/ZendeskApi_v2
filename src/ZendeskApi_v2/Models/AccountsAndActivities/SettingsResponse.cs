// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class SettingsResponse
    {
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }
}