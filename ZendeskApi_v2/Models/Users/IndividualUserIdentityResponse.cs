using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Users
{
    public class IndividualUserIdentityResponse
    {

        [JsonProperty("identity")]
        public UserIdentity Identity { get; set; }
    }
}