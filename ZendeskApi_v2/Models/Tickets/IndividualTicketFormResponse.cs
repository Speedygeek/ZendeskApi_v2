using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class IndividualTicketFormResponse
    {
        [JsonProperty("ticket_form")]
        public TicketForm TicketForm { get; set; }

        //[JsonProperty("audit")]
        //public Audit Audit { get; set; }
    }
}