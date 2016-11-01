using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Votes
{
    public class IndividualVoteResponse
    {
        [JsonProperty("vote")]
        public Vote Vote { get; set; }
    }
}