// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZenDeskApi_v2.Models.Search
{

    public class Result
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Can be: ticket, user, group, organization, or topic
        /// </summary>
        [JsonProperty("result_type")]
        public string ResultType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
