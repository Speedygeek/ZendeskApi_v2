using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Categories
{
    public class IndividualCategoryResponse
    {

        [JsonProperty("category")]
        public Category Category { get; set; }
    }
}