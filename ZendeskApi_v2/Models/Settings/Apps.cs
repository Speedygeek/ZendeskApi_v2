// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Settings
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
