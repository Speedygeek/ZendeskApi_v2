// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.CustomRoles
{

    public class CustomRoles : GroupResponseBase
    {
        [JsonProperty("custom_roles")]
        public IList<CustomRole> CustomRoleCollection { get; set; }
    }
}
