using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Infrastructure.Options;
using TARA.AuthenticationService.Infrastructure.Repositories;
using TARA.AuthenticationService.Infrastructure.Services;

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

        // Options
        services.ConfigureOptions<PasswordOptionsSetup>();
        services.ConfigureOptions<TokenOptionsSetup>();

        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        return services;
    }
}
