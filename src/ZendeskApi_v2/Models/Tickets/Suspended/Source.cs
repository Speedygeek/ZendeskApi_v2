﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets.Suspended
{
    public class Source
    {
        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("to")]
        public To To { get; set; }

        [JsonProperty("rel")]
        public object Rel { get; set; }
    }
}