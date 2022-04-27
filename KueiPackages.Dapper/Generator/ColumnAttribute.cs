namespace KueiPackages.Dapper.Generator
{
    public class ColumnAttribute : Attribute
    {
        public ColumnType ColumnType { get; }

        public ColumnAttribute(ColumnType columnType)
        {
            ColumnType = columnType;
        }
    }
}
