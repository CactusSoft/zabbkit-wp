using System;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.JsonConverters
{
    public class OutputQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Query))
            {
                throw new NotImplementedException();
            }
            var query = (Query) value;
            if (query.Params == null)
            {
                if(query.Value == QueryValues.None)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                string result = query.Value.ToString().ToLower();
                serializer.Serialize(writer, result);
                return;
            }

            serializer.Serialize(writer, query.Params);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
