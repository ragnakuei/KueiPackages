using System.Collections.Concurrent;

namespace KueiPackages.Extensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.TryGetValue(key, out var result))
            {
                return result;
            }

            return default;
        }
    }
}
