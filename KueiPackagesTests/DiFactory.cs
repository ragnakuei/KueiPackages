namespace KueiPackagesTests;

public class DiFactory
{
    private static readonly ServiceProvider _serviceProvider;

    static DiFactory()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json", false, true)
                           .Build();

        services.AddSingleton<IConfiguration>(provider => configuration);

        services.AddTransient<IDbConnection>(provider => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<TestDbContext>(options =>
                                             {
                                                 var connectionString = configuration.GetConnectionString("DefaultConnection");

                                                 options.UseSqlServer(connectionString);

                                                 options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                                             },
                                             contextLifetime: ServiceLifetime.Transient,
                                             optionsLifetime: ServiceLifetime.Transient);

        _serviceProvider = services.BuildServiceProvider();
    }

    public static T GetService<T>()
    {
        return _serviceProvider.GetService<T>();
    }

    public object GetService(Type propPropertyType)
    {
        return _serviceProvider.GetService(propPropertyType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return Enumerable.Empty<object>();
    }
}
