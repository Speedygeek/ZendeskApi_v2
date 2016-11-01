using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{

    public class UploadResult
    {
        [JsonProperty("upload")]
        public Upload Upload { get; set; }
    }
}