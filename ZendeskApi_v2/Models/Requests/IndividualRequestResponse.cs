using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Requests
{
    public class IndividualRequestResponse
    {

        [JsonProperty("request")]
        public Request Request { get; set; }
    }
}