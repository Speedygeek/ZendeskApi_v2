using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class IndividualTicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }
    }
}
