using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Configuration;
using TARA.AuthenticationService.Infrastructure.Repositories;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Infrastructure;
public static class StartupExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PasswordSettings>(options =>
        {
            configuration.GetSection(PasswordSettings.Section).Bind(options);
        });

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}
