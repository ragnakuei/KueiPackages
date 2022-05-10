namespace KueiPackages.Microsoft.AspNetCore.Helpers;

public static class ListViewModelHelper
{
    public static ListDto ToListDto(this ListViewModel listViewModel)
    {
        return new ListDto
               {
                   PageNo          = listViewModel.PageNo,
                   PageSize        = listViewModel.PageSize,
                   SortColumn      = listViewModel.SortColumn,
                   ClickSortColumn = listViewModel.ClickSortColumn,
                   SortColumnOrder = listViewModel.SortColumnOrder,
                   SearchFields    = listViewModel.SearchFields,
               };
    }
}
