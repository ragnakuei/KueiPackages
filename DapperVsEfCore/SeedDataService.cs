namespace DapperVsEfCore
{
    public class SeedDataService
    {
        public Guid AGuid { get; set; } = new Guid("D2B1E299-57C8-42FF-8FC7-070160C3D9DB");

        public void SeedData()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            testDbContext.Database.EnsureCreated();

            testDbContext.A.Add(new A
                                {
                                    Guid = AGuid,
                                    Name = "C",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = AGuid, Name = "C1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = AGuid, Name = "C2", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = AGuid, Name = "C3", },
                                              }
                                });

            var dGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = dGuid,
                                    Name = "D",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = dGuid, Name = "D1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = dGuid, Name = "D2", },
                                              }
                                });

            testDbContext.SaveChanges();
        }

        public void ClearSeedData()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureDeleted();
        }
    }
}
