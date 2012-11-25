using Newtonsoft.Json;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Models.Macros
{
    public class Result
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }
    }
}