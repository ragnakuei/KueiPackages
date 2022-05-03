namespace KueiPackages.Models;

public abstract class BaseResponseDto : ExceptionDto
{
    public string CsrfToken { get; set; }

    public static ResponseDto ShowAlert(string alertMessage = "")
    {
        return new ResponseDto
               {
                   IsFormValid  = true,
                   AlertMessage = alertMessage,
               };
    }

    /// <summary>
    /// 一頁幾筆資料
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 目前頁數
    /// </summary>
    public int PageNo { get; set; } = 1;

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
