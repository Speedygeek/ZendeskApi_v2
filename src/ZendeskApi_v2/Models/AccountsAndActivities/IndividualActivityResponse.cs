using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccountsAndActivities
{
    public class IndividualActivityResponse
    {

        [JsonProperty("activity")]
        public Activity Activity { get; set; }
    }
}