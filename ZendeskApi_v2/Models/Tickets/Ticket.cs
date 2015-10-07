using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{
    public interface ITicket
    {
        object ExternalId { get; set; }
        string Type { get; set; }

        /// <summary>
        /// This field can no longer be set. As of January 2013 use "Comment.Body" instead.
        /// </summary>
        string Description { get; }

        string Subject { get; set; }
        string Priority { get; set; }
        string Status { get; set; }
        string Recipient { get; set; }
        long? RequesterId { get; set; }
        long? SubmitterId { get; set; }
        long? AssigneeId { get; set; }
        long? OrganizationId { get; set; }
        long? GroupId { get; set; }

        /// <summary>
        /// This is for getting the Ids only
        /// </summary>
        IList<long> CollaboratorIds { get; set; }

        /// <summary>
        /// This is used only to update tickets and will not be returned.
        /// NOTE that setting collaborators this way will completely ignore what's already set, so make sure to include existing collaborators in the array if you wish to retain these on the ticket.        
        /// </summary>
        IList<string> CollaboratorEmails { get; set; }

        object ForumTopicId { get; set; }
        object ProblemId { get; set; }
        bool? HasIncidents { get; set; }
        DateTimeOffset? DueAt { get; set; }
        IList<string> Tags { get; set; }
        IList<CustomField> CustomFields { get; set; }
        SatisfactionRating SatisfactionRating { get; set; }
        int? BrandId { get; set; }
        Via Via { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        Comment Comment { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        Requester Requester { get; set; }

        /// <summary>
        /// The id of the ticket form to render for this ticket - only applicable for enterprise accounts.
        /// </summary>
        long? TicketFormId { get; set; }

        string Url { get; set; }
        long? Id { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }

    public class Ticket : BaseTicket, ITicket
    {
        [JsonProperty("external_id")]
        public object ExternalId { get; set; }             

        [JsonProperty("type")]
        public string Type { get; set; }
        
        /// <summary>
        /// This field can no longer be set. As of January 2013 use "Comment.Body" instead.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }        

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        /// <summary>
        /// This is for getting the Ids only
        /// </summary>
        [JsonProperty("collaborator_ids")]
        public IList<long> CollaboratorIds { get; set; }

        /// <summary>
        /// This is used only to update tickets and will not be returned.
        /// NOTE that setting collaborators this way will completely ignore what's already set, so make sure to include existing collaborators in the array if you wish to retain these on the ticket.        
        /// </summary>
        [JsonProperty("collaborators")]
        public IList<string> CollaboratorEmails { get; set; }

        [JsonProperty("forum_topic_id")]
        public object ForumTopicId { get; set; }

        [JsonProperty("problem_id")]
        public object ProblemId { get; set; }

        [JsonProperty("has_incidents")]
        public bool? HasIncidents { get; set; }

        [JsonProperty("due_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? DueAt { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }

        [JsonProperty("brand_id")]
        public int? BrandId { get; set; }
      
        [JsonProperty("via")]
        public Via Via { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        /// <summary>
        /// This is used for updates only
        /// </summary>
        [JsonProperty("requester")]
        public Requester Requester { get; set; }

        /// <summary>
        /// The id of the ticket form to render for this ticket - only applicable for enterprise accounts.
        /// </summary>
        [JsonProperty("ticket_form_id")]
        public long? TicketFormId { get; set; }
        
    }
}
