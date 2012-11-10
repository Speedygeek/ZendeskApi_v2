using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Organizations
{
    public class IndividualOrganizationResponse
    {

        [JsonProperty("organization")]
        public Organization Organization { get; set; }
    }
}