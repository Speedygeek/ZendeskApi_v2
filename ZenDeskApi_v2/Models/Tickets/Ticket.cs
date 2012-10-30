using System.Collections.Generic;
using Newtonsoft.Json;
using ZenDeskApi_v2.Models.Shared;

namespace ZenDeskApi_v2.Models.Tickets
{

    public class Ticket
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("external_id")]
        public object ExternalId { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("requester_id")]
        public int RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public int SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public int? AssigneeId { get; set; }

        [JsonProperty("organization_id")]
        public int? OrganizationId { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("collaborator_ids")]
        public IList<int> CollaboratorIds { get; set; }

        [JsonProperty("forum_topic_id")]
        public object ForumTopicId { get; set; }

        [JsonProperty("problem_id")]
        public object ProblemId { get; set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }

        [JsonProperty("due_at")]
        public string DueAt { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; }

        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}
