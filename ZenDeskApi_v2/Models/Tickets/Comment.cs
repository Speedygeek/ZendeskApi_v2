using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Tickets
{
    public class Comment
    {
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
    }
}