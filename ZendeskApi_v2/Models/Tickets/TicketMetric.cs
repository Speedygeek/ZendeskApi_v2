using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class TicketMetric : BaseTicket
    {
        [JsonProperty("ticket_id")]
        public long? TicketId { get; set; }

        [JsonProperty("group_stations")]
        public long? GroupStations { get; set; }

        [JsonProperty("assignee_stations")]
        public long? AssigneeStations { get; set; }

        [JsonProperty("reopens")]
        public long? Reopens { get; set; }

        [JsonProperty("replies")]
        public long? Replies { get; set; }

        [JsonProperty("assignee_updated_at")]
        public string AssigneeUpdatedAt { get; set; }

        [JsonProperty("requester_updated_at")]
        public string RequesterUpdatedAt { get; set; }

        [JsonProperty("status_updated_at")]
        public string StatusUpdatedAt { get; set; }

        [JsonProperty("initially_assigned_at")]
        public string InitiallyAssignedAt { get; set; }

        [JsonProperty("assigned_at")]
        public string AssignedAt { get; set; }

        [JsonProperty("solved_at")]
        public string SolvedAt { get; set; }

        [JsonProperty("latest_comment_added_at")]
        public string LatestCommentAddedAt { get; set; }

        [JsonProperty("first_resolution_time_in_minutes")]
        public TimeSpanMetric FirstResolutionTimeInMinutes { get; set; }

        [JsonProperty("reply_time_in_minutes")]
        public TimeSpanMetric ReplyTimeInMinutes { get; set; }

        [JsonProperty("full_resolution_time_in_minutes")]
        public TimeSpanMetric FullResolutionTimeInMinutes { get; set; }

        [JsonProperty("agent_wait_time_in_minutes")]
        public TimeSpanMetric AgentWaitTimeInMinutes { get; set; }

        [JsonProperty("requester_wait_time_in_minutes")]
        public TimeSpanMetric RequesterWaitTimeInMinutes { get; set; }
    }
}
