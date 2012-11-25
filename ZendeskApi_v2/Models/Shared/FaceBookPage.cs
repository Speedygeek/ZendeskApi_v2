using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public  class FacebookPage
    {
        /// <summary>
        /// Used when event is FacebookEvent
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Used when event is FacebookEvent
        /// </summary>
        [JsonProperty("graph_id")]
        public string GraphId { get; set; } 
    }
}