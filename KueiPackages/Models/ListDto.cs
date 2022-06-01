namespace KueiPackages.Models;

public class ListDto
{
    /// <summary>
    /// 頁碼
    /// <para>Base 1</para>
    /// </summary>
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// 一頁筆數
    /// </summary>
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// 排序欄位
    /// </summary>
    public string? SortColumn { get; set; }

    /// <summary>
    /// 前端決定的要排序的欄位
    /// </summary>
    public string? ClickSortColumn { get; set; }

    /// <summary>
    /// 排序欄位順序
    /// </summary>
    public SortColumnOrder SortColumnOrder { get; set; } = SortColumnOrder.Asc;
    
    /// <summary>
    /// 搜尋關鍵字
    /// </summary>
    public Dictionary<string, string>? SearchFields { get; set; }
    
    public int FromRowNo => (PageNo - 1) * PageSize + 1;

    public int ToRowNo => PageNo * PageSize;
}
