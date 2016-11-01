// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Macros
{
    public class ApplyMacroResponse
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }
}