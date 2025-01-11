using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Configuration;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Infrastructure.Repositories;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Infrastructure;
public static class StartupExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddDbContext<EventStoreDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("EventStoreConnection"));
        });

        // Settings
        services.Configure<PasswordSettings>(options =>
        {
            configuration.GetSection(PasswordSettings.Section).Bind(options);
        });

        // Services
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
