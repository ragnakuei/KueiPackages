namespace KueiPackages.Microsoft.AspNetCore.Models;

/// <summary>
/// 當傳入字串包含 SQL Injection 字元時，就以此 Exception 回應
/// </summary>
public class SqlInjectionValidateFailedException : Exception
{
    public SqlInjectionValidateFailedException(string message)
        : base(message)
    {
    }
    
    public SqlInjectionValidateFailedException(string message, string content)
        : base(message)
    {
        Content = content;
    }

    public string Content { get; }
}
