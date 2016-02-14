using System;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{

    public class CustomField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]        
        public object Value { get; set; }
    }    
}
