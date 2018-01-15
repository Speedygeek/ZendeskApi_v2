using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ZendeskApi_v2.Serialization
{
    public class ZendeskContractResolver : DefaultContractResolver
    {
        public static readonly ZendeskContractResolver Instance = new ZendeskContractResolver();

        public ZendeskContractResolver()
        {
            this.NamingStrategy = new SnakeCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                property.DefaultValueHandling = DefaultValueHandling.Include;
            }

            return property;
        }
    }
}
