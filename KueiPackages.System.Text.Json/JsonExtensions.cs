using System.Text.Json;

namespace KueiPackages.System.Text.Json
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(obj, jsonSerializerOptions);
        }

        public static T ParseJson<T>(this string json, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
        }
    }
}
