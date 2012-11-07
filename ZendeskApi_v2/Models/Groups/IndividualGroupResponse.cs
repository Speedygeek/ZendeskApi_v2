using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class IndividualGroupResponse
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}