using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class IndividualTicketMetricResponse
    {
        [JsonProperty("ticket_metric")]
        public TicketMetric TicketMetric{ get; set; }
    }
}
