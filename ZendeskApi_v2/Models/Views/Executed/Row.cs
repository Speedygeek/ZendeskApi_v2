// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Views.Executed
{

    public class Row
    {

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("assignee_id")]
        public long AssigneeId { get; set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; }

        [JsonProperty("ticket")]
        public ExecutedTicket ExecutedTicket { get; set; }
    }
}
