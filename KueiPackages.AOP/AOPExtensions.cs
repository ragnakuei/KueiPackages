using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KueiPackages.AOP
{
    public static class AOPExtensions
    {
        public static void AddAOPScoped<T, TImpl>(this IServiceCollection services)
            where T : class
            where TImpl : class, T
        {
            services.AddScoped<TImpl>();
        
            services.AddScoped<T>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<T>>();
                T   obj    = provider.GetRequiredService<TImpl>();
                return AOP<T>.Create(obj, logger);
            });
        }
    }
}