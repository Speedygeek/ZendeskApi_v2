using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class IndividualUserRelatedInformationResponse
    {
        [JsonProperty("user_related")]
        public RelatedInformation UserRelated { get; set; }
    }
}
