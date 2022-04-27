namespace KueiPackages.Microsoft.AspNetCore.Models;

public class ValidateFormFailedException : Exception
{
    public ValidateFormFailedException()
    {
    }

    public ValidateFormFailedException(string message)
        : base(message)
    {
    }

    public Dictionary<string, List<string>>? ValidateResult { get; set; }
}
