using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class AccountOverviewResponse
    {
        [JsonProperty("account_overview")]
        public AccountOverview AccountOverview { get; set; }
    }
}
