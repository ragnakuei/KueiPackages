namespace KueiPackages.Dapper.Models;

public class AlertException : Exception
{
    public string AlertMessage { get; }

    public AlertException(string alertMessage, string? message = null) : base(message)
    {
        AlertMessage = alertMessage;
    }
}