// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.SharingAgreements;

namespace ZendeskApi_v2.Models.SharingAgreements
{

    public class GroupSharingAgreementResponse : GroupResponseBase
    {

        [JsonProperty("sharing_agreements")]
        public IList<SharingAgreement> SharingAgreements { get; set; }
    }
}
