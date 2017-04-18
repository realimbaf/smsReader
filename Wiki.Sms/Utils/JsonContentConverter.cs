using System;
using Newtonsoft.Json;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Utils
{
    public class JsonContentConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null)
                return null;
            var source = serializer.Deserialize<DTOBeelineMessages>(reader);
            foreach (var message in source.messages)
            {
                message.content = DecodeMessage.Decode(message.content);
            }
            return source;
            
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DTOBeelineMessages));
        }
    }
}
