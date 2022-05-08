namespace KueiPackages.Helpers;

public static class StringHelpers
{
    /// <summary>
    /// 增加 offset timeSpan 字串
    /// </summary>
    public static DateTime ToDateTime(string s, DateTime defaultValue = default)
    {
        if(DateTime.TryParse(s, out DateTime result))
        {
            return result;
        }
        
        return defaultValue;
    }
}
