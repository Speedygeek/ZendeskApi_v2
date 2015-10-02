using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Models.Groups
{
    public interface IGroup
    {
        string Url { get; set; }
        long? Id { get; set; }
        string Name { get; set; }
        bool Deleted { get; set; }
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }

    public class Group : IGroup
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
