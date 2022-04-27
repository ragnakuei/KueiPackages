namespace DapperVsEfCore;

public class DapperService
{
    private readonly IDbConnection   _dbConnection;
    private readonly SeedDataService _seedDataService;

    public DapperService(IDbConnection   dbConnection,
                         SeedDataService seedDataService)
    {
        _dbConnection    = dbConnection;
        _seedDataService = seedDataService;
    }

    public A Run()
    {
        var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid

SELECT *
FROM [dbo].[ADetail]
WHERE [AGuid] = @Guid
";
        var param = new { Guid = _seedDataService.AGuid };

        var boxDto = _dbConnection.QueryMultipleResult(sql, param)
                                  .Result(reader =>
                                          {
                                              var readerResult = reader.ReadFirstOrDefault<A>();
                                              if (readerResult != null)
                                              {
                                                  readerResult.Details = reader.Read<ADetail>().ToArray();
                                              }
                                              return readerResult;
                                          });

        return boxDto;
    }
}
