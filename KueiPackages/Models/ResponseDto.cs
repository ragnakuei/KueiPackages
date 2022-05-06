namespace KueiPackages.Models;

public class ResponseDto : BaseResponseDto
{
    public object? Data { get; set; }
}

public static class ResponseDtoHelper
{
    public static ResponseDto ShowAlert(string alertMessage = "")
    {
        return new ResponseDto
               {
                   IsFormValid  = true,
                   AlertMessage = alertMessage,
               };
    }
}

public class ResponseDto<T> : BaseResponseDto
{
    public T Data { get; set; }
}
