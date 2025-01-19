using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Infrastructure.Repositories;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.AuthenticationService.Infrastructure.Settings;

namespace TARA.AuthenticationService.Infrastructure;
public static class StartupExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isTestEnviroment = false)
    {
        if (!isTestEnviroment)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // Settings
        services.Configure<PasswordSettings>(options =>
        {
            configuration.GetSection(PasswordSettings.Section).Bind(options);
        });

        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        return services;
    }
}
