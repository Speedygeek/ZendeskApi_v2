// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Apps
    {

        [JsonProperty("use", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Use { get; set; }

        [JsonProperty("create_private", DefaultValueHandling=DefaultValueHandling.Include)]
        public bool CreatePrivate { get; set; }

        [JsonProperty("create_public", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool CreatePublic { get; set; }
    }
}
