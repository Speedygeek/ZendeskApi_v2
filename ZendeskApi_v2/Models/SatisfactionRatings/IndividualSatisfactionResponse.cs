using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Satisfaction
{
    public class IndividualSatisfactionResponse
    {

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}