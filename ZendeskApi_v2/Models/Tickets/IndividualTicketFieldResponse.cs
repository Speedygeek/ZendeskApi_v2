using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class IndividualTicketFieldResponse
    {
        [JsonProperty("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}