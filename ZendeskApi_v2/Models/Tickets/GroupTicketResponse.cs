using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class GroupTicketResponse : GroupResponseBase
    {

        [JsonProperty("tickets")]
        public IList<Ticket> Tickets { get; set; }
    }
}
