using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{

    public class Source
    {

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("to")]
        public To To { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }
    }
}
