namespace KueiPackages.Microsoft.AspNetCore.Models;

public class ListViewModel
{
    /// <summary>
    /// 頁碼
    /// <para>Base 1</para>
    /// </summary>
    [FromQuery]
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// 一頁筆數
    /// </summary>
    [FromQuery]
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// 排序欄位
    /// </summary>
    [PreventSqlInjection]
    public string SortColumn { get; set; }

    /// <summary>
    /// 前端決定的要排序的欄位
    /// </summary>
    [PreventSqlInjection]
    public string ClickSortColumn { get; set; }

    /// <summary>
    /// 排序欄位順序
    /// </summary>
    public SortColumnOrder SortColumnOrder { get; set; } = SortColumnOrder.Asc;
    
    /// <summary>
    /// 搜尋關鍵字
    /// </summary>
    [PreventSqlInjection]
    public Dictionary<string, string> SearchFields { get; set; }
}
