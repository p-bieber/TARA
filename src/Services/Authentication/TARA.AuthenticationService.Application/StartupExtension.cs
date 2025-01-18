using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Application.Services;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure;

namespace TARA.AuthenticationService.Application;
public static class StartupExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration, bool isTestEnviroment = false)
    {
        services.RegisterInfrastructureServices(configuration, isTestEnviroment);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(ApplicationAssemblyReference.Assembly);
        });

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
