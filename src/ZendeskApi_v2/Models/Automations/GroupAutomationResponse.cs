using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Automations
{
    public class GroupAutomationResponse : GroupResponseBase
    {
        [JsonProperty("automations")]
        public IList<Automation> Automations { get; set; }
    }
}
