using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace ZendeskApi_v2.Serialization
{
    public class UnixEpochTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
#if PORTABLE
            bool nullable = (objectType.GetTypeInfo().IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
#else
            bool nullable = (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
#endif

            Type T = nullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType.Name}.");
                }
                return null;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                long epoch = (long)reader.Value;
                return _epoch.AddSeconds(epoch);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
