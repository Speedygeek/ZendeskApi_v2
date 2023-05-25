using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{    
    /// <summary>
    /// No triggers are run on the imported tickets. As a result, there won't be any detailed ticket metrics for the tickets. We recommend setting a tag to signify that these tickets were added to Zendesk using import.
    /// </summary>
    public class TicketImport : Ticket
    {
        /// <summary>
        /// Attachments are handled the same way as in the tickets API. You upload the files then supply the token in the comment parameters.
        /// </summary>
        [JsonProperty("comments")]
        public IList<TicketImportComment> Comments { get; set; }

        [Obsolete("Comment property is not used in TicketImport.  Please use Description property and Comments collection property.",true)]
        public new Comment Comment { get; set; }

        [JsonProperty("description")]
        public new string Description { get; set; }

        /// <summary>
        /// Only Used for Ticket Import
        /// </summary>
        [JsonProperty("solved_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? SolvedAt { get; set; }
    }
}
