using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Comments
{
    public class IndividualCommentsResponse
    {
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}