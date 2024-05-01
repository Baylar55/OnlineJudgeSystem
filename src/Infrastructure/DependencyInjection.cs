using AlgoCode.Application.Common.Interfaces;
using AlgoCode.Application.Features.Tags.Commands.CreateTag;
using AlgoCode.Infrastructure.Data;
using AlgoCode.Infrastructure.Data.Interceptors;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoCode.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add<ValidationFilter>();
            //})
            //.AddFluentValidation(options =>
            //{
            //    options.RegisterValidatorsFromAssemblyContaining<ISaveChangesInterceptor>();
            //});
            //services.AddFluentValidation();
            services.AddFluentValidationAutoValidation();
            services.AddTransient<IValidator<CreateTagCommand>, CreateTagCommandValidator>();
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

#if (UseSQLite)
            options.UseSqlite(connectionString);
#else
                options.UseSqlServer(connectionString);
#endif
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();

#if (UseApiOnly)
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
#else
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
#endif

            services.AddSingleton(TimeProvider.System);
            //services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
