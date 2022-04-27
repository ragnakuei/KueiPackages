using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreateDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                                   {
                                       services.AddHostedService<App>();
                                       services.AddScoped<TestDbContextFactory>();
                                   })
                .Build()
                .Run();
        }
    }

    public class App : IHostedService
    {
        private readonly ILogger<App> _logger;

        private readonly IHostApplicationLifetime _appLifetime;

        private readonly IHostEnvironment _env;

        private readonly IConfiguration                     _configuration;
        private readonly TestDbContextFactory _testDbContextFactory;

        public App(ILogger<App>                       logger,
                   IHostApplicationLifetime           appLifetime,
                   IHostEnvironment                   env,
                   IConfiguration                     configuration,
                   TestDbContextFactory testDbContextFactory)
        {
            _logger                             = logger;
            _appLifetime                        = appLifetime;
            _env                                = env;
            _configuration                      = configuration;
            _testDbContextFactory = testDbContextFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App running at: {time}",                  DateTimeOffset.Now);
            _logger.LogInformation("App running at Env: {env}",               _env.EnvironmentName);

            using (var dbContext = _testDbContextFactory.CreateDbContext(null))
            {
                var db = dbContext.Database;
                await db.MigrateAsync(cancellationToken);
            }

            _appLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App stopped at: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
