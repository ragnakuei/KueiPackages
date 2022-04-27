namespace KueiPackages.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue? GetValueOrNull<TKey, TValue>(this Dictionary<TKey?, TValue?> dict, TKey? key)
            where TKey : struct
            where TValue : struct
        {
            if (dict                        == null
             || key.GetValueOrNull() == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue? GetValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue?> dict, TKey key)
            where TKey : class
            where TValue : struct
        {
            if (dict == null
             || key  == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue GetValueOrNull<TKey, TValue>(this Dictionary<TKey?, TValue> dict, TKey? key)
            where TKey : struct
            where TValue : class
        {
            if (dict                        == null
             || key.GetValueOrNull() == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue GetValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TKey : class
            where TValue : class
        {
            if (dict == null
             || key  == null)
            {
                return null;
            }

            return dict?.GetValueOrDefault(key);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.TryGetValue(key, out var result))
            {
                return result;
            }

            return default;
        }
    }
}
