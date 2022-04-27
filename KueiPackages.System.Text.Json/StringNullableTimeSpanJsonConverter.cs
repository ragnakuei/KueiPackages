using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KueiPackages.System.Text.Json;

public class StringNullableTimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader    reader,
                                   Type                  typeToConvert,
                                   JsonSerializerOptions options)
    {
        if (TimeSpan.TryParse(reader.GetString(), out var result))
        {
            return result;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter        writer,
                               TimeSpan?             nullableTimeSpan,
                               JsonSerializerOptions options) =>
        writer.WriteStringValue(nullableTimeSpan?.ToString());
}
