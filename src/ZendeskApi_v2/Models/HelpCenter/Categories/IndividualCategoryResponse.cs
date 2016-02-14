using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Categories
{
   public class IndividualCategoryResponse
   {

      [JsonProperty("category")]
      public Category Category { get; set; }
   }
}