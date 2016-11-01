// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Organizations
{
    public class GroupOrganizationResponse : GroupResponseBase
    {
        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }
    }
}