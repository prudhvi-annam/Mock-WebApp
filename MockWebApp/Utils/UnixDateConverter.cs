using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MockWebApp.Utils
{
    public class UnixDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)reader.Value).ToLocalTime().DateTime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {            
            if (value is DateTime dt)
                writer.WriteRawValue(((DateTimeOffset)dt).ToUnixTimeSeconds().ToString());

        }
    }
}