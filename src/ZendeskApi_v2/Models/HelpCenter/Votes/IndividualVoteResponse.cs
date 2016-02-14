using Newtonsoft.Json;
using ZendeskApi_v2.Models.HelpCenter.Votes;

namespace ZendeskApi_v2.Models.HelpCenter.Votes
{
   public class IndividualVoteResponse
   {

      [JsonProperty("vote")]
      public Vote Vote { get; set; }
   }
}