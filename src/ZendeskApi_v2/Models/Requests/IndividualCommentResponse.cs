using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Requests
{
    public class IndividualCommentResponse
    {
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}