namespace KueiPackages.Dapper.Generator
{
    public enum ColumnType
    {
        None = 0,

        /// <summary>
        /// 主鍵 / 唯一鍵
        /// </summary>
        Sn = 1,

        /// <summary>
        /// 關聯至別表的 主鍵 / 唯一鍵
        /// </summary>
        Fn = 2,

        /// <summary>
        /// 用來標示為刪除的 Column
        /// </summary>
        DeleteFlag = 3,
    }
}
