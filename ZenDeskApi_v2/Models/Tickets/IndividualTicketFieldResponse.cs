using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class IndividualTicketFieldResponse
    {
        [JsonProperty("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}