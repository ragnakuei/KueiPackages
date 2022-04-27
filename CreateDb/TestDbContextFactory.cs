using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CreateDb
{
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        private readonly IConfiguration _configuration;

        public TestDbContextFactory()
        {
            _configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();
        }

        public TestDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TestDbContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var optionBuilder = new DbContextOptionsBuilder<TestDbContext>()
                               .UseSqlServer(connectionString,
                                             builder =>
                                             {
                                                 builder.CommandTimeout(2400);
                                                 builder.EnableRetryOnFailure(2);
                                                 builder.MigrationsHistoryTable("_MigrationsHistory", DbParameter.DefaultSchema);
                                                 builder.MigrationsAssembly("CreateDb");
                                             })
                               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new TestDbContext(optionBuilder.Options);
        }
    }
}
