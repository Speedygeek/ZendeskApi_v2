using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.AccountsAndActivities
{
    public class IndividualActivityResponse
    {

        [JsonProperty("activity")]
        public Activity Activity { get; set; }
    }
}