using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Users
{
    public class User : IndividualSearchableResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("shared")]
        public bool? Shared { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("locale_id")]
        public long? LocaleId { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("last_login_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? LastLoginAt { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("shared_phone_number")]
        public bool? SharedPhoneNumber { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("role_type")]
        public long? RoleType { get; set; }

        [JsonProperty("custom_role_id")]
        public long? CustomRoleId { get; set; }

        [JsonProperty("moderator")]
        public bool Moderator { get; set; }

        [JsonProperty("ticket_restriction")]
        public string TicketRestriction { get; set; }

        [JsonProperty("only_private_comments")]
        public bool OnlyPrivateComments { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        [JsonProperty("photo")]
        public Photo Photo { get; set; }

        [JsonProperty("remote_photo_url")]
        public string RemotePhotoUrl { get; set; }

        [JsonProperty("user_fields")]
        public IDictionary<string, object> CustomFields { get; set; }

        [JsonProperty("abilities")]
        /// <summary>
        ///  used in sideloading and can not be used to update or change the users
        ///  abilities
        /// </summary>
        public dynamic Abilities { get; set; }
    }
}
