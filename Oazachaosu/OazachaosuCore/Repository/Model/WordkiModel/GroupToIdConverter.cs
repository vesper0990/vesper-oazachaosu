using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Repository
{
    internal class GroupToIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Group group = value as Group;
            if(group == null)
            {
                throw new Exception("Passed object is not Group type");
            }
            writer.WriteToken(JsonToken.Integer, group.Id);
        }
    }
}