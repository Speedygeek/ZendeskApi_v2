using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class RelatedInformation
    {

        [JsonProperty("assigned_tickets")]
        public int AssignedTickets { get; set; }
     
        [JsonProperty("requested_tickets")]
        public int RequestedTickets { get; set; }

        [JsonProperty("ccd_tickets")]
        public int CcdTickets { get; set; }

        [JsonProperty("topics")]
        public int Topics { get; set; }

        [JsonProperty("topic_comments")]
        public int TopicComments { get; set; }

        [JsonProperty("votes")]
        public int Votes { get; set; }

        [JsonProperty("subscriptions")]
        public int Subscriptions { get; set; }

        [JsonProperty("entry_subscriptions")]
        public int EntrySubscriptions { get; set; }

        [JsonProperty("forum_subscriptions")]
        public int FormSubscriptions { get; set; }
 
        [JsonProperty("organization_subscriptions")]
        public int OrganitzationSubscriptions { get; set; }
    }
}
