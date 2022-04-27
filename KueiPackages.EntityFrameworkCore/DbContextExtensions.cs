using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KueiPackages.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static QueryMultipleBuilder QueryMultiple(this DbContext dbContext,
                                                         string         sql,
                                                         SqlParameter[] parameters     = null,
                                                         CommandType    sqlCommandType = CommandType.Text)
        {
            return new(dbContext, sql, parameters, sqlCommandType);
        }
    }
}
