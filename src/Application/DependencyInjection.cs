using AlgoCode.Application.Common.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AlgoCode.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        MappingConfig.ConfigureMappings();

        return services;
    }
}
