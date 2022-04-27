namespace KueiPackages.Dapper
{
    internal class QueryMultipleHandler
    {
        private IDbConnection _dbConnection;
        private string        _sql;
        private object        _param;

        internal QueryMultipleHandler(IDbConnection dbConnection, string sql, object param)
        {
            _dbConnection = dbConnection;
            _sql          = sql;
            _param        = param;
        }

        internal T Result<T>(Func<SqlMapper.GridReader, T> readerFunc)
        {
            var reader = _dbConnection.QueryMultiple(_sql, _param);

            return readerFunc.Invoke(reader);
        }

        internal void Result(Action<SqlMapper.GridReader> readerAction)
        {
            var reader = _dbConnection.QueryMultiple(_sql, _param);

            readerAction.Invoke(reader);
        }
    }
}
