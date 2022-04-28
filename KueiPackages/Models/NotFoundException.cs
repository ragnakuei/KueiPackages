namespace KueiPackages.Models;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
