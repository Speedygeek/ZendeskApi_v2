using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.Tickets
{
    public class Ticket : BaseTicket
    {
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

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

        [JsonProperty("assignee_email")]
        public string AssigneeEmail { get; set; }

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
        /// NOTE that setting collaborators this way will completely ignore what's already set,
        /// so make sure to include existing collaborators in the array
        /// if you wish to retain these on the ticket.
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
        public long? BrandId { get; set; }

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

        /// <summary>
        /// Is true if any comments are public, false otherwise
        /// </summary>
        [JsonProperty("is_public")]
        public bool IsPublic { get; private set; }

        /// <summary>
        /// The id of a closed ticket for a follow-up ticket.
        /// For More info <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#creating-follow-up-tickets"/>
        /// </summary>
        [JsonProperty("via_followup_source_id")]
        public long ViaFollowupSourceId { get; set; }

        /// <summary>
        /// Comment Count on the ticket when sideloaded
        /// </summary>
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }
        /// <summary>
        /// Incident Counts on the ticket when sideloaded
        /// </summary>
        [JsonProperty("incident_count")]
        public int? IncidentCount { get; set; }

        /// <summary>
        /// Any follow-up tickets to this ticket.
        /// </summary>
        [JsonProperty("followup_ids")]
        public IList<long> FollowUpIds { get; set; }

#nullable enable
        /// <summary>
        /// Dates on the ticket when sideloaded
        /// </summary>
        [JsonProperty("dates")]
        public object? Dates { get; set; }
#nullable disable        
    }
}
