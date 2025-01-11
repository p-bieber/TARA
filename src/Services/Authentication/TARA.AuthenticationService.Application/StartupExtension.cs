using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Application.Services;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure;

namespace TARA.AuthenticationService.Application;
public static class StartupExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructureServices(configuration);

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(typeof(StartupExtension).Assembly);
        });

        services.AddScoped<PasswordValidator>();
        services.AddScoped<PasswordFactory>();

        services.AddScoped<UserFactory>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
