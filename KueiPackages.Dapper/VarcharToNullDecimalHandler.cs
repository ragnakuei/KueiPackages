namespace KueiPackages.Dapper
{
    /// <summary>
    /// varchar to / from decimal?
    /// </summary>
    public class VarcharToNullDecimalHandler : SqlMapper.TypeHandler<decimal?>
    {
        // Read From Db
        public override decimal? Parse(object value)
        {
            return value?.ToString()?.ToNullableDecimal();
        }

        // Write To Db
        public override void SetValue(IDbDataParameter parameter, decimal? value)
        {
            parameter.Value  = value?.ToString();
            parameter.DbType = DbType.AnsiString;
        }
    }
}
