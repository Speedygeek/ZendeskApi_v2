// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Categories
{
    public class GroupCategoryResponse : GroupResponseBase
    {
        [JsonProperty("categories")]
        public IList<Category> Categories { get; set; }
    }
}