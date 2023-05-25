using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.HelpCenter.Topics;

namespace ZendeskApi_v2.Models.UserSegments
{
    public class GroupUserSegmentResponse : GroupResponseBase
    {
        [JsonProperty("user_segments")]
        public IList<UserSegment> UserSegments { get; set; }

        [JsonProperty("sections")]
        public IList<Sections.Section> Sections { get; set; }

        [JsonProperty("topics")]
        public IList<Topic> Topics { get; set; }
    }
}
