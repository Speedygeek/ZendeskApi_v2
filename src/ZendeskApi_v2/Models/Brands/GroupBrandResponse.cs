// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Brands
{
    public class GroupBrandResponse : GroupResponseBase
    {
        [JsonProperty("brands")]
        public IList<Brand> Brands { get; set; }
    }
}