using System;
using System.Text.Json;

namespace KueiPackages.System.Text.Json;

public abstract class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
{
    protected abstract string DateTimeFormat { get; set; }

    public override DateTime? Read(ref Utf8JsonReader    reader,
                                   Type                  typeToConvert,
                                   JsonSerializerOptions options)
    {
        if (DateTime.TryParse(reader.GetString(), out var result))
        {
            return result;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter        writer,
                               DateTime?             nullableTimeSpan,
                               JsonSerializerOptions options) =>
        writer.WriteStringValue(nullableTimeSpan?.ToString(DateTimeFormat));
}
