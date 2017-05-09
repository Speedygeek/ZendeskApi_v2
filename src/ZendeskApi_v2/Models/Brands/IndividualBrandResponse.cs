using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Brands
{
    public class IndividualBrandResponse
    {
        [JsonProperty("brand")]
        public Brand Brand { get; set; }
    }
}