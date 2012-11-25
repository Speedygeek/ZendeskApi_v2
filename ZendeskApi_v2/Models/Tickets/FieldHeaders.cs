using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class FieldHeaders
    {

        [JsonProperty("generated_timestamp")]
        public string GeneratedTimestamp { get; set; }

        [JsonProperty("req_name")]
        public string ReqName { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("submitter_name")]
        public string SubmitterName { get; set; }

        [JsonProperty("assignee_name")]
        public string AssigneeName { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("current_tags")]
        public string CurrentTags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("via")]
        public string Via { get; set; }

        [JsonProperty("ticket_type")]
        public string TicketType { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("assigned_at")]
        public string AssignedAt { get; set; }

        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("initially_assigned_at")]
        public string InitiallyAssignedAt { get; set; }

        [JsonProperty("solved_at")]
        public string SolvedAt { get; set; }

        [JsonProperty("resolution_time")]
        public string ResolutionTime { get; set; }

        [JsonProperty("satisfaction_score")]
        public string SatisfactionScore { get; set; }

        [JsonProperty("group_stations")]
        public string GroupStations { get; set; }

        [JsonProperty("assignee_stations")]
        public string AssigneeStations { get; set; }

        [JsonProperty("reopens")]
        public string Reopens { get; set; }

        [JsonProperty("replies")]
        public string Replies { get; set; }

        [JsonProperty("first_reply_time_in_minutes")]
        public string FirstReplyTimeInMinutes { get; set; }

        [JsonProperty("first_reply_time_in_minutes_within_business_hours")]
        public string FirstReplyTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("first_resolution_time_in_minutes")]
        public string FirstResolutionTimeInMinutes { get; set; }

        [JsonProperty("first_resolution_time_in_minutes_within_business_hours")]
        public string FirstResolutionTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("full_resolution_time_in_minutes")]
        public string FullResolutionTimeInMinutes { get; set; }

        [JsonProperty("full_resolution_time_in_minutes_within_business_hours")]
        public string FullResolutionTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("agent_wait_time_in_minutes")]
        public string AgentWaitTimeInMinutes { get; set; }

        [JsonProperty("agent_wait_time_in_minutes_within_business_hours")]
        public string AgentWaitTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("requester_wait_time_in_minutes")]
        public string RequesterWaitTimeInMinutes { get; set; }

        [JsonProperty("requester_wait_time_in_minutes_within_business_hours")]
        public string RequesterWaitTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("on_hold_time_in_minutes")]
        public string OnHoldTimeInMinutes { get; set; }

        [JsonProperty("on_hold_time_in_minutes_within_business_hours")]
        public string OnHoldTimeInMinutesWithinBusinessHours { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("req_external_id")]
        public string ReqExternalId { get; set; }

        [JsonProperty("req_email")]
        public string ReqEmail { get; set; }

        [JsonProperty("req_id")]
        public string ReqId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}