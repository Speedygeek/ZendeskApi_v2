using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class TicketResponse
    {

        [JsonProperty("tickets")]
        public IList<Ticket> Tickets { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("previous_page")]
        public object PreviousPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
