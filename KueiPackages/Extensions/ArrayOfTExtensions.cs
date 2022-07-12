using KueiPackages.Types;

namespace KueiPackages.Extensions;

public static class ArrayOfTExtensions
{
    public static Array<T> ToArrayOf<T>(this IEnumerable<T> source)
    {
        return new Array<T>(source);
    }

    public static Array<T> ToArrayOf<T>(this T[] source)
    {
        return new Array<T>(source);
    }
}
