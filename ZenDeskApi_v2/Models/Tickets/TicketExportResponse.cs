using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class TicketExportResponse
    {

        [JsonProperty("results")]
        public IList<TicketExportResult> Results { get; set; }

        [JsonProperty("field_headers")]
        public FieldHeaders FieldHeaders { get; set; }

        [JsonProperty("options")]
        public Options Options { get; set; }

        [JsonProperty("end_time")]
        public int EndTime { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }
    }
}