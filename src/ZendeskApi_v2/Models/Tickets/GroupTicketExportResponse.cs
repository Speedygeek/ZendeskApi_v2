using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2.Models.Groups;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.SharingAgreements;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Models.Tickets
{
    public class GroupTicketExportResponse
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("end_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset EndTime { get; set; }

        [JsonProperty("groups")]
        public IList<Group> Groups { get; set; }

        [JsonProperty("last_audits")]
        public IList<Audit> LastAudits { get; set; }

        [JsonProperty("metric_sets")]
        public IList<TicketMetric> MetricSets { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("end_of_stream")]
        public bool EndOfStream { get; set; }

        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }

        [JsonProperty("sharing_agreements")]
        public IList<SharingAgreement> SharingAgreements { get; set; }

        [JsonProperty("ticket_forms")]
        public IList<TicketForm> TicketForms { get; set; }

        [JsonProperty("tickets")]
        public IList<Ticket> Tickets { get; set; }

        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}
