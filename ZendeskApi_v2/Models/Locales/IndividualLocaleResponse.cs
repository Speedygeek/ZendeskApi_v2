using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Locales
{
    public class IndividualLocaleResponse
    {
        [JsonProperty("locale")]
        public Locale Locale { get; set; }
    }
}