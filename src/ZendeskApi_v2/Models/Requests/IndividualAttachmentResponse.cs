using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Requests
{
    public class IndividualAttachmentResponse
    {
        [JsonProperty("attachment")]
        public Attachment Attachment { get; set; }
    }
}
