using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Organizations
{
    public class IndividualOrganizationResponse
    {

        [JsonProperty("organization")]
        public Organization Organization { get; set; }
    }
}