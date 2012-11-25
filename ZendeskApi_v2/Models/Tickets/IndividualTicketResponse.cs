using Newtonsoft.Json;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{
    public class IndividualTicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }

        [JsonProperty("audit")]
        public Audit Audit { get; set; }
    }
}
