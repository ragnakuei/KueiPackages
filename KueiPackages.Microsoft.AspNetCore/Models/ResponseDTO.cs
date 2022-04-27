namespace KueiPackages.Microsoft.AspNetCore.Models;

public abstract class BaseResponseDTO
{
    /// <summary>
    /// 表單是否驗証成功
    /// </summary>
    public bool IsFormValid { get; set; }

    public string? Message { get; set; }

    /// <summary>
    /// 顯示 Alert Message
    /// </summary>
    public string? AlertMessage { get; set; }

    public string? ErrorCode { get; set; }

    public string CsrfToken { get; set; }

    public static ResponseDTO ShowAlert(string alertMessage = "")
    {
        return new ResponseDTO
               {
                   IsFormValid  = true,
                   AlertMessage = alertMessage,
               };
    }
}

public class ResponseDTO : BaseResponseDTO
{
    public object? Data { get; set; }

    public static ResponseDTO Ok(object? o = null, string messsage = "")
    {
        return new ResponseDTO
               {
                   IsFormValid = true,
                   Message     = messsage,
                   Data        = o
               };
    }
}

public class ResponseDTO<T> : BaseResponseDTO
{
    public T Data { get; set; }

    public static ResponseDTO<T> Ok(T o, string messsage = "")
    {
        return new ResponseDTO<T>
               {
                   IsFormValid = true,
                   Message     = messsage,
                   Data        = o
               };
    }
}
