// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Settings
{

    public class User
    {

        [JsonProperty("tagging")]
        public bool Tagging { get; set; }
    }
}
