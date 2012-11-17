using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tags
{
    public class TagAutocompleteResponse
    {
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }
    }
}