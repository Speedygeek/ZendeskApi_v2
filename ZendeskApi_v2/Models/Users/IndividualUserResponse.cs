using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class IndividualUserResponse
    {

        [JsonProperty("user")]
        public User User { get; set; }
    }
}