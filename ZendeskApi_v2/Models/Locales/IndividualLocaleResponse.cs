using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Locales
{
    public class IndividualLocaleResponse
    {
        [JsonProperty("locale")]
        public Locale Locale { get; set; }
    }
}