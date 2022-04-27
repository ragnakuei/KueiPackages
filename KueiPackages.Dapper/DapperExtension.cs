namespace KueiPackages.Dapper;

public static class DapperExtension
{
    public static QueryMultipleBuilder QueryMultipleResult(this IDbConnection dbConnection,
                                                           string             sql,
                                                           object             param = null)
    {
        return new(dbConnection, sql, param);
    }
}
