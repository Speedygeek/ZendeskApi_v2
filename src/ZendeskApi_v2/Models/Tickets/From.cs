﻿using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Tickets
{
    public class From
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Will be populated when Ticket.Via.Channel = 'Voice'. See https://github.com/mozts2005/ZendeskApi_v2/issues/158.
        /// </summary>
        [JsonProperty("formatted_phone")]
        public string FormattedPhone { get; set; }

        /// <summary>
        /// Will be populated when Ticket.Via.Channel = 'Voice'. See https://github.com/mozts2005/ZendeskApi_v2/issues/158.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Will be populated when Ticket is a follow-up ticket. See https://github.com/Speedygeek/ZendeskApi_v2/issues/427.
        /// </summary>
        [JsonProperty("ticket_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long? TicketId { get; set; }
    }
}
