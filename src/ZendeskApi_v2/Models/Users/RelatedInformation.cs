using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Users
{
    public class RelatedInformation
    {
        [JsonProperty("assigned_tickets")]
        public long AssignedTickets { get; set; }

        [JsonProperty("requested_tickets")]
        public long RequestedTickets { get; set; }

        [JsonProperty("ccd_tickets")]
        public long CcdTickets { get; set; }

        [JsonProperty("topics")]
        public long Topics { get; set; }

        [JsonProperty("topic_comments")]
        public long TopicComments { get; set; }

        [JsonProperty("votes")]
        public long Votes { get; set; }

        [JsonProperty("subscriptions")]
        public long Subscriptions { get; set; }

        [JsonProperty("entry_subscriptions")]
        public long EntrySubscriptions { get; set; }

        [JsonProperty("forum_subscriptions")]
        public long FormSubscriptions { get; set; }

        [JsonProperty("organization_subscriptions")]
        public long OrganitzationSubscriptions { get; set; }
    }
}
