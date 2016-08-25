﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{

    public class Lotus
    {

        [JsonProperty("prefer_lotus", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool PreferLotus { get; set; }

        [JsonProperty("reporting", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Reporting { get; set; }
    }
}
