using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Schedules
{
    public class Holiday
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_date")]
        public int? StartDate { get; set; }

        [JsonProperty("end_date")]
        public int? EndDate { get; set; }
    }
}