using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class GroupTicketMetricResponse: GroupResponseBase
    {

        [JsonProperty("ticket_metrics")]
        public IList<TicketMetric> TicketMetrics { get; set; }
    }
}
