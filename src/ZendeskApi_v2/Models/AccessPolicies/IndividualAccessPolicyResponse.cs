using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.AccessPolicies
{
    public class IndividualAccessPolicyResponse
    {
        [JsonProperty("access_policy")]
        public AccessPolicy AccessPolicy { get; set; }
    }
}
