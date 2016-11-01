// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class Lotus
    {
        [JsonProperty("prefer_lotus")]
        public bool PreferLotus { get; set; }

        [JsonProperty("reporting")]
        public bool Reporting { get; set; }
    }
}