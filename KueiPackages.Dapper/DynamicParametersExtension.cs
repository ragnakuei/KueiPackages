namespace KueiPackages.Dapper;

public static class DynamicParametersExtension
{
    private static Dictionary<SqlDbType, DbType?> _typeMapping
        = new()
          {
              [SqlDbType.VarChar]          = DbType.AnsiString,
              [SqlDbType.NVarChar]         = DbType.String,
              [SqlDbType.BigInt]           = DbType.Int64,
              [SqlDbType.Binary]           = DbType.Binary,
              [SqlDbType.Bit]              = DbType.Boolean,
              [SqlDbType.Char]             = DbType.AnsiStringFixedLength,
              [SqlDbType.DateTime]         = DbType.DateTime,
              [SqlDbType.Decimal]          = DbType.Decimal,
              [SqlDbType.Float]            = DbType.Double,
              [SqlDbType.Image]            = DbType.Binary,
              [SqlDbType.Int]              = DbType.Int32,
              [SqlDbType.Money]            = DbType.Currency,
              [SqlDbType.NChar]            = DbType.StringFixedLength,
              [SqlDbType.NText]            = DbType.String,
              [SqlDbType.Real]             = DbType.Single,
              [SqlDbType.UniqueIdentifier] = DbType.Guid,
              [SqlDbType.SmallDateTime]    = DbType.DateTime,
              [SqlDbType.SmallInt]         = DbType.Int16,
              [SqlDbType.SmallMoney]       = DbType.Decimal,
              [SqlDbType.Text]             = DbType.String,
              [SqlDbType.Timestamp]        = DbType.Binary,
              [SqlDbType.TinyInt]          = DbType.Byte,
              [SqlDbType.VarBinary]        = DbType.Binary,
              [SqlDbType.Variant]          = DbType.Object,
              [SqlDbType.Xml]              = DbType.Xml,
              // [SqlDbType.Udt]              = DbType.,
              // [SqlDbType.Structured]       = DbType.,
              [SqlDbType.Date]           = DbType.Date,
              [SqlDbType.Time]           = DbType.Time,
              [SqlDbType.DateTime2]      = DbType.DateTime2,
              [SqlDbType.DateTimeOffset] = DbType.DateTimeOffset,
          };

    public static void Add(this DynamicParameters parameters,
                           string                 name,
                           object                 value,
                           SqlDbType              sqlDbType,
                           ParameterDirection?    direction = null,
                           int?                   size      = null,
                           byte?                  precision = null,
                           byte?                  scale     = null)
    {
        var dbType = _typeMapping.GetValueOrDefault(sqlDbType);

        if (dbType.HasValue == false)
        {
            throw new NotSupportedException();
        }

        parameters.Add(name, value, dbType, direction, size, precision, scale);
    }
}
