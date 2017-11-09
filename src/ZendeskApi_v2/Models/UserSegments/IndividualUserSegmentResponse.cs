using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace ZendeskApi_v2.Models.UserSegments
{
    public class IndividualUserSegmentResponse
    {
        [JsonProperty("user_segment")]
        public UserSegment UserSegment { get; set; }

    }
}
