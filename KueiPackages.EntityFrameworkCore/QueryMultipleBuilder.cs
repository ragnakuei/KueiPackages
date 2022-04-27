using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KueiPackages.EntityFrameworkCore
{
    public class QueryMultipleBuilder
    {
        private readonly QueryMultipleHandler _handler;

        internal QueryMultipleBuilder(DbContext      dbContext,
                                      string         sql,
                                      SqlParameter[] parameters     = null,
                                      CommandType    sqlCommandType = CommandType.Text)
        {
            _handler = new QueryMultipleHandler(dbContext, sql, parameters, sqlCommandType);
        }

        public TResult Result<TResult>(Func<DbDataReader, TResult> readerFunc)
        {
            return _handler.Result(readerFunc);
        }

        public void Result(Action<DbDataReader> readerAction)
        {
            _handler.Result(readerAction);
        }
    }
}
