using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{
    public class Comment
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

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

        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; private set; }

        [JsonProperty("via")]
        public Via Via { get; private set; }

        [JsonProperty("metadata")]
        public MetaData MetaData { get; private set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

    }

}
