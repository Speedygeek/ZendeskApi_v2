using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class BulkUpdate
    {
        [JsonProperty("type")]
        public string Type { get; set; }        

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        /// <summary>
        /// This is used only to update tickets and will not be returned.
        /// NOTE that setting collaborators this way will completely ignore what's already set, so make sure to include existing collaborators in the array if you wish to retain these on the ticket.        
        /// </summary>
        [JsonProperty("collaborators")]
        public IList<string> CollaboratorEmails { get; set; }
    }
}