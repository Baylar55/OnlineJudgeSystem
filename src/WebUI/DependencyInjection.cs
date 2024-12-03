using AlgoCode.Infrastructure.Data;
using AlgoCode.WebUI.Infrastructure;
using AlgoCode.WebUI.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AlgoCode.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();
        services.AddScoped<IErrorHandlingService, ErrorHandlingService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IConfigurationManager, ConfigurationManager>();
        services.AddSingleton<IFileService, FileService>();

        services.AddDbContext<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();
        return services;
    }
}
