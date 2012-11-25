using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views
{
    public class GroupViewCountResponse
    {
        [JsonProperty("view_counts")]
        public IList<ViewCount> ViewCounts { get; set; }
    }
}