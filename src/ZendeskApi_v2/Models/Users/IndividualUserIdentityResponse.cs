using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class IndividualUserIdentityResponse
    {

        [JsonProperty("identity")]
        public UserIdentity Identity { get; set; }
    }
}