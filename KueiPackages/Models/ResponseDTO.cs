namespace KueiPackages.Models;

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

    /// <summary>
    /// 一頁幾筆資料
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// 目前頁數
    /// </summary>
    public int? PageNo { get; set; }

    /// <summary>
    /// 資料總筆數
    /// </summary>
    public int DataCount { get; set; }
    
    /// <summary>
    /// 總頁數
    /// </summary>
    public int? PageCount
    {
        get
        {
            if (DataCount % PageSize == 0)
            {
                return DataCount / PageSize;
            }

            return (DataCount / PageSize) + 1;
        }
    }
}

public class ResponseDTO : BaseResponseDTO
{
    public object? Data { get; set; }
}

public class ResponseDTO<T> : BaseResponseDTO
{
    public T Data { get; set; }
}
