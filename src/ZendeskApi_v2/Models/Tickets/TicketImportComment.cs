using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TicketImportComment
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Used for uploading attachments only
        /// When creating and updating tickets you may attach files by passing in an array of the tokens received from uploading the files. 
        /// For the upload attachment to succeed when updating a ticket, a comment must be included.
        /// Use Attachments.UploadAttachment to get the token first.
        /// </summary>
        [JsonProperty("uploads")]
        public IList<string> Uploads { get; set; }        

        /// <summary>
        /// Used only for getting ticket comments
        /// </summary>
        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
