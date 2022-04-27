namespace KueiPackages.Dapper
{
    public class QueryMultipleBuilder
    {
        private readonly QueryMultipleHandler _handler;

        internal QueryMultipleBuilder(IDbConnection dbConnection, string sql, object param)
        {
            _handler = new QueryMultipleHandler(dbConnection, sql, param);
        }

        public T Result<T>(Func<SqlMapper.GridReader, T> readerFunc)
        {
            return _handler.Result(readerFunc);
        }

        public void Result(Action<SqlMapper.GridReader> readerAction)
        {
            _handler.Result(readerAction);
        }
    }
}
