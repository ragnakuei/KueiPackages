namespace CreateDb
{
    public static class SqlDbTypes
    {
        public static string Varchar(int maxLength)
            => maxLength > 8000 ? "varchar(8000)" : $"varchar({maxLength})";

        public const string Varcharmax = "varchar(max)";

        public static string Nvarchar(int maxLength)
            => maxLength > 4000 ? "nvarchar(4000)" : $"nvarchar({maxLength})";

        public const string Nvarcharmax = "nvarchar(max)";

        public static string Char(int maxLength)
            => maxLength > 8000 ? "char(8000)" : $"char({maxLength})";

        public const string Charmax = "char(max)";

        public static string Nchar(int maxLength)
            => maxLength > 4000 ? "nchar(4000)" : $"nchar({maxLength})";

        public const string Ncharmax = "nchar(max)";

        public const string Bit = "bit";

        public const string Tinyint  = "tinyint";
        public const string Smallint = "smallint";
        public const string Int      = "int";
        public const string Bigint   = "bigint";

        public static string Decimal(int precision, int scale) => $"decimal({precision},{scale})";
        public static string Numeric(int precision, int scale) => $"numeric({precision},{scale})";

        public static string Float(int precision) => $"float({precision})";

        public const string Date             = "date";
        public const string Datetime         = "datetime";
        public const string Datetime2        = "datetime2";
        public const string Datetimeoffset   = "datetimeoffset";
        public const string Timestamp        = "timestamp";
        public const string Time             = "time";
        public const string Uniqueidentifier = "uniqueidentifier";

        public static string Varbinary(int maxLength) => $"varbinary({maxLength})";

        public static string Binary(int maxLength) => $"binary({maxLength})";

        public const string Hierarchyid = "hierarchyid";
    }
}