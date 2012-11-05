using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Users
{
    public class IndividualUserResponse
    {

        [JsonProperty("user")]
        public User User { get; set; }
    }
}