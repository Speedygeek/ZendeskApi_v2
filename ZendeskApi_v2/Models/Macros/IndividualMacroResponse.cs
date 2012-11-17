using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Macros
{
    public class IndividualMacroResponse
    {
        [JsonProperty("macro")]
        public Macro Macro { get; set; }
    }
}