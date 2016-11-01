using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Translations
{
    public class IndividualTranslationResponse
    {
        [JsonProperty("translation")]
        public Translation Translation { get; set; }
    }
}