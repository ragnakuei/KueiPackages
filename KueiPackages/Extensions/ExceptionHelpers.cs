namespace KueiPackages.Extensions;

public static class ExceptionHelpers
{
    /// <summary>
    /// 取得 Exception Log
    /// 由內而外顯示
    /// </summary>
    public static string GetLogMessages(this Exception ex)
    {
        var result = new StringBuilder();

        if (ex.InnerException != null)
        {
            result.AppendLine(ex.InnerException.GetLogMessages());
            result.AppendLine("---------------------------------------------");
        }

        if (ex.Message.IsNullOrWhiteSpace() == false)
        {
            result.AppendLine($"Message:{ex.Message}");
        }

        result.AppendLine($"StackTrace:{ex.StackTrace}");

        return result.ToString();
    }
}
