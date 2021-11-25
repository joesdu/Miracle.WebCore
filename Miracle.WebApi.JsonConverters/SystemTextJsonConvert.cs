using System.Text.Json;
using System.Text.Json.Serialization;

namespace Miracle.WebApi.JsonConverters;
public class SystemTextJsonConvert
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Convert.ToDateTime(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
    public class DateTimeNullConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : Convert.ToDateTime(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options) => writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
    }
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => TimeSpan.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(@"HH\:mm\:ss"));
    }
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => TimeOnly.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(@"HH\:mm\:ss"));
    }
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateOnly.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}