using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Infrastructure.Configuration;
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

        services.AddScoped<PasswordHasher>();

        return services;
    }
}
