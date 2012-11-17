// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Settings
{

    public class Lotus
    {

        [JsonProperty("prefer_lotus")]
        public bool PreferLotus { get; set; }

        [JsonProperty("reporting")]
        public bool Reporting { get; set; }
    }
}
