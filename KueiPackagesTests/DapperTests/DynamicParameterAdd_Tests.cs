namespace KueiPackagesTests.DapperTests
{
    public class DynamicParameterAdd_Tests
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
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Name] = @name
";
            var parameters = new DynamicParameters();
            parameters.Add("@name", "C", SqlDbType.NVarChar, size: 50);

            var dbConnection = DiFactory.GetService<IDbConnection>();

            var aDto = dbConnection.QueryFirstOrDefault<A>(sql, parameters);

            Assert.AreEqual("C", aDto.Name);
        }
    }
}
