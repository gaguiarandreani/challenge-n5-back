using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SecurityApi.Application.Mappings;
using System.Reflection;

namespace SecurityApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CommonMappingsProfile));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
