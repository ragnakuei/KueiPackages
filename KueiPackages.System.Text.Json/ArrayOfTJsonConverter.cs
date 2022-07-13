using System;
using System.Reflection;
using System.Text.Json;
using KueiPackages.Types;

namespace KueiPackages.System.Text.Json;

public class ArrayOfTJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        if (typeToConvert.GetGenericTypeDefinition() != typeof(Array<>))
        {
            return false;
        }

        return true;
    }

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
    {
        Type keyType = type.GetGenericArguments()[0];

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(typeof(ArrayOfTConverterInner<>).MakeGenericType(new Type[] { keyType }),
                                                                          BindingFlags.Instance | BindingFlags.Public,
                                                                          binder: null,
                                                                          args: new object[] { options },
                                                                          culture: null)!;

        return converter;
    }

    private class ArrayOfTConverterInner<T> : JsonConverter<Array<T>>
    {
        private readonly JsonConverter<T[]> _valueConverter;

        public ArrayOfTConverterInner(JsonSerializerOptions options)
        {
            _valueConverter = (JsonConverter<T[]>)options.GetConverter(typeof(T[]));
        }

        public override Array<T> Read(ref Utf8JsonReader    reader,
                                      Type                  typeToConvert,
                                      JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var values = _valueConverter.Read(ref reader, typeof(T[]), options);

            return new Array<T>(values);
        }

        public override void Write(Utf8JsonWriter writer, Array<T> value, JsonSerializerOptions options)
        {
            _valueConverter.Write(writer, (T[])value, options);
        }
    }
}
