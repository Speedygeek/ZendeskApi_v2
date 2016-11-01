﻿using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class GetSatisfactionTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "get_satisfaction_target";
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }
    }
}