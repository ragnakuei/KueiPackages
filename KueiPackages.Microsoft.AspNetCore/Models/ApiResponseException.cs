namespace KueiPackages.Microsoft.AspNetCore.Models;

public class ApiResponseException : Exception
{
    public ApiResponseException(ResponseDTO? responseDto)
    {
        ResponseDTO = responseDto;
    }

    public ResponseDTO? ResponseDTO { get; }
}
