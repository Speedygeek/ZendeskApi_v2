using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class GroupTicketFormResponse : GroupResponseBase
    {

        [JsonProperty("ticket_forms")]
        public IList<TicketForm> TicketForms { get; set; }
    }
}