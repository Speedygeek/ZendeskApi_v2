using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Post
{
    public class IndividualPostResponse
    {
        [JsonProperty("Post")]
        public Post Post { get; set; }
    }
}
