namespace KueiPackages.Models;

public abstract class BaseResponseDto : ExceptionDto
{
    public string CsrfToken { get; set; }

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
}
