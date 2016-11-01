// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Locales
{
    public class GroupLocaleResponse : GroupResponseBase
    {
        [JsonProperty("locales")]
        public IList<Locale> Locales { get; set; }
    }
}