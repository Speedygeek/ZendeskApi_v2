﻿// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets.Suspended
{
    public class Author
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}