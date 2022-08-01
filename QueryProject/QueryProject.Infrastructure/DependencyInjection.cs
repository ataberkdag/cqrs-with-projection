using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryProject.Application.Services.Interfaces;
using QueryProject.Infrastructure.Services;

namespace QueryProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetConnectionString("Cache");
            });

            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
