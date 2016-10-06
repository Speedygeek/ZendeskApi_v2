using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZendeskApi_v2.Models.Users
{
    public class IndividualUserRelatedInformationResponse
    {
        [JsonProperty("user_related")]
        public RelatedInformation UserRelated { get; set; }
    }
}
