namespace KueiPackages.Models;

public class ApiResponseException : Exception
{
    public ApiResponseException(ExceptionDto? exceptionDTO)
    {
        ExceptionDTO = exceptionDTO;
    }

    public ExceptionDto? ExceptionDTO { get; }
}
