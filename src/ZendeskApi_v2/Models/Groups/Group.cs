using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Groups
{
    public class Group : IndividualSearchableResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
}