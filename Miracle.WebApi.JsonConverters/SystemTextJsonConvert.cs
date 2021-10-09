using System.Text.Json;
using System.Text.Json.Serialization;

namespace Miracle.WebApi.JsonConverters
{
    public class SystemTextJsonConvert
    {
        public class DecimalConverter : JsonConverter<decimal>
        {
            public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType == JsonTokenType.Number ? reader.GetDecimal() : decimal.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
        }

        public class DecimalNullConverter : JsonConverter<decimal?>
        {
            public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
                reader.TokenType == JsonTokenType.Number
                    ? reader.GetDecimal()
                    : string.IsNullOrEmpty(reader.GetString()) ? default(decimal?) : decimal.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options) => writer.WriteStringValue(value?.ToString());
        }


        public class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateTime.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public class DateTimeNullConverter : JsonConverter<DateTime?>
        {
            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options) => writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public class TimeSpanJsonConvert : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => TimeSpan.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }

        public class IntNullConverter : JsonConverter<int?>
        {
            public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
                reader.TokenType == JsonTokenType.Number
                    ? reader.GetInt32()
                    : string.IsNullOrEmpty(reader.GetString()) ? default(int?) : int.Parse(reader.GetString());

            public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
            {
                if (value != null) writer.WriteNumberValue(value.Value);
                else writer.WriteNullValue();
            }
        }

        public class BoolConverter : JsonConverter<bool>
        {
            public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
                reader.TokenType is JsonTokenType.True or JsonTokenType.False
                    ? reader.GetBoolean()
                    : reader.TokenType == JsonTokenType.String
                    ? bool.Parse(reader.GetString())
                    : reader.TokenType == JsonTokenType.Number
                    ? reader.GetDouble() > 0
                    : throw new NotImplementedException($"un processed tokentype {reader.TokenType}");

            public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        }

        public class BoolNullConverter : JsonConverter<bool?>
        {
            public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
                reader.TokenType is JsonTokenType.True or JsonTokenType.False
                    ? reader.GetBoolean()
                    : reader.TokenType == JsonTokenType.Null
                    ? null
                    : reader.TokenType == JsonTokenType.String
                    ? bool.Parse(reader.GetString())
                    : reader.TokenType == JsonTokenType.Number
                    ? reader.GetDouble() > 0
                    : throw new NotImplementedException($"un processed tokentype {reader.TokenType}");

            public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
            {
                if (value != null) writer.WriteBooleanValue(value.Value);
                else writer.WriteNullValue();
            }
        }
    }
}