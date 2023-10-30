using SecurityApi.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using SecurityApi.Interfaces.Services;
using SecurityApi.Infrastructure.Services;
using Interfaces.Repositories;
using SecurityApi.Infrastructure.Persistence.Repositories;
using Interfaces.Producers;
using Producers;
using Microsoft.Extensions.Configuration;

namespace SecurityApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<AppDbContext>();
        services.AddTransient<AppDbContextOptionsBuilder>();
        services.AddSingleton(config);

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionProducer, PermissionProducer>();

        return services;
    }
}
