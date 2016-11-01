using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Translations
{
    public class GroupTranslationResponse : GroupResponseBase
    {
        [JsonProperty("translations")]
        public IList<Translations.Translation> Translations { get; set; }
    }
}