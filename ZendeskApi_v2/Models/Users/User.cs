using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Users
{
    public interface IUser
    {
        long? Id { get; set; }
        string Url { get; set; }
        string Name { get; set; }
        string ExternalId { get; set; }
        string Alias { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        bool? Active { get; set; }
        bool Verified { get; set; }
        bool? Shared { get; set; }
        long? LocaleId { get; set; }
        string TimeZone { get; set; }
        DateTimeOffset? LastLoginAt { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Signature { get; set; }
        string Details { get; set; }
        string Notes { get; set; }
        long? OrganizationId { get; set; }
        string Role { get; set; }
        long? CustomRoleId { get; set; }
        bool Moderator { get; set; }
        string TicketRestriction { get; set; }
        bool OnlyPrivateComments { get; set; }
        IList<string> Tags { get; set; }
        bool Suspended { get; set; }
        Photo Photo { get; set; }
        string RemotePhotoUrl { get; set; }
        IDictionary<string, object> CustomFields { get; set; }
    }

    public class User : IUser
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("shared")]
        public bool? Shared { get; set; }

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
    }
}
