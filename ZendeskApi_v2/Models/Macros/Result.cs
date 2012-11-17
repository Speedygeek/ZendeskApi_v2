using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Models.Macros
{
    public class Result
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }
    }
}