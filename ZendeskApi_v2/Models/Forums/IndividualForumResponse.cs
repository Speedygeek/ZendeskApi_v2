using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Forums
{
    public class IndividualForumResponse
    {
        [JsonProperty("forum")]
        public Forum Forum { get; set; }
    }
}