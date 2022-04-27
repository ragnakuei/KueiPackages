namespace KueiPackagesTests.DapperTests
{
    public class VarcharToNullDecimalHandlerTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void 使用匿名型別()
        {
            Dapper.SqlMapper.AddTypeHandler(new VarcharToNullDecimalHandler());

            var sql = @"
DECLARE @TmpTable TABLE
                  (
                      [Column1] VARCHAR(50)
                  )

INSERT INTO @TmpTable(Column1)
VALUES (@Column1)

SELECT *
FROM @TmpTable
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var dto = new TestDto
                      {
                          Column1 = 1.234m,
                      };

            var param = new DynamicParameters();
            param.AddDynamicParams(dto);

            var result = dbConnection.QueryFirstOrDefault<TestDto>(sql, param);

            Assert.AreEqual(result.Column1, 1.234m);
        }

        [Test]
        public void 指定型別()
        {
            Dapper.SqlMapper.AddTypeHandler(new VarcharToNullDecimalHandler());

            var sql = @"
DECLARE @TmpTable TABLE
                  (
                      [Column1] VARCHAR(50)
                  )

INSERT INTO @TmpTable(Column1)
VALUES (@Column1)

SELECT *
FROM @TmpTable
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var dto = new TestDto
                      {
                          Column1 = 1.234m,
                      };

            var param = new DynamicParameters();
            param.Add("Column1", dto.Column1, DbType.AnsiString);

            var result = dbConnection.QueryFirstOrDefault<TestDto>(sql, param);

            Assert.AreEqual(result.Column1, 1.234m);
        }

        private class TestDto
        {
            public decimal? Column1 { get; set; }
        }
    }
}
