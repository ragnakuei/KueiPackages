using System;
using System.Text.Json;

namespace KueiPackages.System.Text.Json;

public abstract class DateTimeJsonConverter : JsonConverter<DateTime>
{
    protected abstract string DateTimeFormat { get; set; }

    public override DateTime Read(ref Utf8JsonReader    reader,
                                  Type                  typeToConvert,
                                  JsonSerializerOptions options)
    {
        if (DateTime.TryParse(reader.GetString(), out var result))
        {
            return result;
        }

        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter        writer,
                               DateTime              dateTime,
                               JsonSerializerOptions options) =>
        writer.WriteStringValue(dateTime.ToString(DateTimeFormat));
}
