using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Automations
{
    public class IndividualAutomationResponse
    {
        [JsonProperty("automation")]
        public Automation Automation { get; set; }
    }
}
