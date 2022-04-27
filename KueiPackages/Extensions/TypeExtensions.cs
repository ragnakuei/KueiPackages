namespace KueiPackages.Extensions;

public static class TypeExtensions
{
    public static bool IsDictionary(this Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }

    public static bool IsDictionaryValueType(this Type t, Type valueOfType)
    {
        var  argumentTypes = t.GetGenericArguments();
        if(argumentTypes.Length != 2)
        {
            return false;
        }
        
        var keyType       = argumentTypes[0];
        var valueType     = argumentTypes[1];

        return valueType == valueOfType;
    }
}
