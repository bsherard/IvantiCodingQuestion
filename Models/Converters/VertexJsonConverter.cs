using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IvantiCodingQuestion.Models
{
    public class VertexJsonConverter : JsonConverter<(int X, int Y)>
    {
        public override (int X, int Y) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            (int X, int Y) result;
            int x;
            int y;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.PropertyName || !reader.GetString().Equals(nameof(result.X), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException();
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            x = reader.GetInt32();

            reader.Read();

            if (reader.TokenType != JsonTokenType.PropertyName || !reader.GetString().Equals(nameof(result.Y), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException();
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            y = reader.GetInt32();

            reader.Read();

            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            result = (x, y);

            return result;
        }

        public override void Write(Utf8JsonWriter writer, (int X, int Y) value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber(JsonEncodedText.Encode(nameof(value.X)), value.X);
            writer.WriteNumber(JsonEncodedText.Encode(nameof(value.Y)), value.Y);
            writer.WriteEndObject();
        }
    }
}
