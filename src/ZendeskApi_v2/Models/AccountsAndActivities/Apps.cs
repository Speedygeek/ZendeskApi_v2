// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Apps
    {

        [JsonProperty("use")]
        public bool Use { get; set; }

        [JsonProperty("create_private")]
        public bool CreatePrivate { get; set; }

        [JsonProperty("create_public")]
        public bool CreatePublic { get; set; }
    }
}
