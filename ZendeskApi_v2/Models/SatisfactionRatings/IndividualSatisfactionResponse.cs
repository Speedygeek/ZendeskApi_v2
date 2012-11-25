using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Satisfaction
{
    public class IndividualSatisfactionResponse
    {

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}