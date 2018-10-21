using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Subscriptions
{
    public class IndividualSubscriptionResponse
    {
        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }
    }
}