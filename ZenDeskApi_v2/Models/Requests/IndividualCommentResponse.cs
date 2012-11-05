using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Requests
{
    public class IndividualCommentResponse
    {
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }
}