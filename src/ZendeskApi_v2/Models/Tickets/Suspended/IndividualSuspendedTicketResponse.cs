using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets.Suspended
{
    public class IndividualSuspendedTicketResponse
    {
        [JsonProperty("suspended_ticket")]
        public SuspendedTicket SuspendedTicket { get; set; }
    }
}