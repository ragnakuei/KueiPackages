namespace DapperVsEfCore
{
    public class EfCoreService
    {
        private readonly TestDbContext   _dbContext;
        private readonly SeedDataService _seedDataService;

        public EfCoreService(TestDbContext   dbContext,
                             SeedDataService seedDataService)
        {
            _dbContext       = dbContext;
            _seedDataService = seedDataService;
        }

        public A QueryMultiple()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid

SELECT *
FROM [dbo].[ADetail]
WHERE [AGuid] = @Guid
";


            var sqlParameters = new[]
                                {
                                    new SqlParameter("Guid", _seedDataService.AGuid),
                                };

            var boxDto = _dbContext.QueryMultiple(sql, sqlParameters)
                                   .Result(reader =>
                                           {
                                               var tempResult = reader.TranslateFirstOrDefault<A>();
                                               if (tempResult != null)
                                               {
                                                   tempResult.Details = reader.Translate<ADetail>().ToArray();
                                               }

                                               return tempResult;
                                           });

            return boxDto;
        }

        public A FromSqlRaw_SqlParameter()
        {
            var sqlParameters = new[]
                                {
                                    new SqlParameter("Guid", _seedDataService.AGuid),
                                };

            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid
";

            var boxDto = _dbContext.A.FromSqlRaw(sql, sqlParameters)
                                   .Include(a => a.Details)
                                   .AsNoTracking()
                                   .FirstOrDefault();

            return boxDto;
        }

        public A FromSqlRaw_Where()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
";

            var boxDto = _dbContext.A.FromSqlRaw(sql)
                                   .Where(a => a.Guid == _seedDataService.AGuid)
                                   .Include(a => a.Details)
                                   .AsNoTracking()
                                   .FirstOrDefault();

            return boxDto;
        }
    }
}
