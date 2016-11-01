using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Sections
{
    public class GroupSectionResponse : GroupResponseBase
    {
        [JsonProperty("sections")]
        public IList<Section> Sections { get; set; }
    }
}