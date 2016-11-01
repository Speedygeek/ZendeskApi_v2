using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZendeskApi_v2.Serialization
{
    public class ZendeskContractResolver : DefaultContractResolver
    {
        public static readonly ZendeskContractResolver Instance = new ZendeskContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                property.DefaultValueHandling = DefaultValueHandling.Include;
            }

            return property;
        }
    }
}