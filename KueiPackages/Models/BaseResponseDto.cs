namespace KueiPackages.Models;

public abstract class BaseResponseDto
{
    public string CsrfToken { get; set; }

    /// <summary>
    /// 表單是否驗証成功
    /// </summary>
    public bool IsFormValid { get; set; }

    public string Message { get; set; }

    /// <summary>
    /// 顯示 Alert Message
    /// </summary>
    public string AlertMessage { get; set; }

    public string ErrorCode { get; set; }
    
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

    public int PageOffset { get; set; } = 5;

    public string? Keyword { get; set; }

    /// <summary>
    /// 用來判斷目前的 PageNo 是否為合理狀況 !
    /// </summary>
    public bool Validate => DataCount == 0 || PageNo <= ValidMaxPageNo;

    public int ValidMaxPageNo => DataCount == 0
                                     ? 0
                                     : (int)Math.Ceiling((double)DataCount / PageSize);
}
