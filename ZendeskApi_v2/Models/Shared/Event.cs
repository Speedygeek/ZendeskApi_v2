using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public class Event
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }        

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// Used when event is Comment
        /// </summary>
        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        /// <summary>
        /// Used when event is Comment
        /// </summary>
        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }

        /// <summary>
        /// Used when event is Comment
        /// </summary>
        [JsonProperty("trusted")]
        public bool? Trusted { get; set; }

        /// <summary>
        /// Used when event is VoiceComment
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Used when event is VoiceComment
        /// </summary>
        [JsonProperty("formatted_from")]
        public string FormattedFrom { get; set; }

        /// <summary>
        /// Used when event is VoiceComment
        /// </summary>
        [JsonProperty("transcription_visible")]
        public string TranscriptionVisible { get; set; }

        /// <summary>
        /// Used when event is CommentPrivacyChange
        /// </summary>
        [JsonProperty("comment_id")]
        public long? CommentId { get; set; }

        /// <summary>
        /// Used when event is CreateTicket or Change
        /// </summary>
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        /// <summary>
        /// Used when event is CreateTicket or Change
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }

        /// <summary>
        /// Used when event is Change
        /// </summary>
        [JsonProperty("previous_value")]
        public object PreviousValue { get; set; }

        /// <summary>
        /// Used when event is Notification
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Used when event is Notification
        /// </summary>
        [JsonProperty("via")]
        public Via Via { get; set; }

        /// <summary>
        /// Used when event is Notification
        /// </summary>
        [JsonProperty("recipients")]
        public IList<string> Recipients { get; set; }

        /// <summary>
        /// Used when event is Error
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Used when event is External
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// Used when event is External
        /// </summary>
        [JsonProperty("resource")]
        public string Resource { get; set; }

        /// <summary>
        /// Used when event is FacebookEvent
        /// </summary>
        [JsonProperty("communication")]
        public long? Communication { get; set; }

        /// <summary>
        /// Used when event is FacebookEvent
        /// </summary>
        [JsonProperty("ticket_via")]
        public string TicketVia { get; set; }

        /// <summary>
        /// Used when event is FacebookEvent
        /// </summary>
        [JsonProperty("page")]
        public FacebookPage FacebookPage { get; set; }

        /// <summary>
        /// Used when event is Push
        /// </summary>
        [JsonProperty("value_reference")]
        public string ValueReference { get; set; }

        /// <summary>
        /// Used when event is SatisfactionRating
        /// </summary>
        [JsonProperty("assignee_id")]
        public string AssigneeId { get; set; }

        /// <summary>
        /// Used when event is SatisfactionRating
        /// </summary>
        [JsonProperty("score")]
        public string Score { get; set; }

        /// <summary>
        /// Used when event is Tweet
        /// </summary>
        [JsonProperty("direct_message")]
        public string DirectMessage { get; set; }

        /// <summary>
        /// Used when event is SMS
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Used when event is SMS
        /// </summary>
        [JsonProperty("recipient_id")]
        public string RecipientId { get; set; }

        /// <summary>
        /// Used when event is TicketSharingEvent
        /// </summary>
        [JsonProperty("agreement_id")]
        public string AgreementId { get; set; }

        /// <summary>
        /// Used when event is TicketSharingEvent
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
    }
}