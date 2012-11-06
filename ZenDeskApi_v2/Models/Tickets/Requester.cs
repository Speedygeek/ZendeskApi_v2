using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class Requester
    {
        /// <summary>
        /// Optional
        /// See ZendeskApi.Locale for more info
        /// </summary>
        [JsonProperty("locale_id")]
        public long LocaleId { get; set; }

        /// <summary>
        /// If the email already exists in the system this is optional
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}