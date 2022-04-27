namespace KueiPackagesTests.EntityFrameworkCoreTests
{
    public class QueryMultiple_Result_Action_Tests
    {
        private Guid _dGuid;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            testDbContext.Database.EnsureCreated();

            var aGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = aGuid,
                                    Name = "C",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C2", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C3", },
                                              }
                                });

            _dGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = _dGuid,
                                    Name = "D",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _dGuid, Name = "D1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _dGuid, Name = "D2", },
                                              }
                                });

            testDbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void QueryMultiple_無參數()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Name] = N'C'

SELECT [d].*
FROM [dbo].[ADetail] [d]
    JOIN [dbo].[A] [a]
         ON [d].[AGuid] = [a].[Guid]
WHERE [a].[Name] = N'C'
";

            var boxDto = new A();

            testDbContext.QueryMultiple(sql)
                         .Result(reader =>
                                 {
                                     boxDto         = reader.TranslateFirstOrDefault<A>();
                                     boxDto.Details = reader.Translate<ADetail>().ToArray();
                                 });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 3);
        }

        [Test]
        public void QueryMultiple_具名參數()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

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
                                    new SqlParameter("Guid", _dGuid),
                                };

            var boxDto = new A();

            testDbContext.QueryMultiple(sql, sqlParameters)
                         .Result(reader =>
                                 {
                                     boxDto         = reader.TranslateFirstOrDefault<A>();
                                     boxDto.Details = reader.Translate<ADetail>().ToArray();
                                 });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 2);
        }

        [Test]
        public void QueryMultiple_無參數_TranslateFirstOrDefault_查不到資料()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Name] = N'Z'
";

            var boxDto = new A();

            testDbContext.QueryMultiple(sql)
                         .Result(reader =>
                                 {
                                     boxDto = reader.TranslateFirstOrDefault<A>();
                                 });

            Assert.True(boxDto == null);
        }

        [Test]
        public void QueryMultiple_無參數_Translate_查不到資料()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Name] = N'Z'
";

            var boxDto = new A[0];

            testDbContext.QueryMultiple(sql)
                         .Result(reader =>
                                 {
                                     reader.Translate<A>().ToArray();
                                 });

            Assert.True(boxDto.Length == 0);
        }
    }
}
