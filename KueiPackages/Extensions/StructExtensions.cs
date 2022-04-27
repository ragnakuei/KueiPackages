namespace KueiPackages.Extensions
{
    public static class StructExtensions
    {
        public static T? GetValueOrNull<T>(this T? t) where T : struct
        {
            if (t == null)
            {
                return null;
            }

            return t;
        }
    }
}
