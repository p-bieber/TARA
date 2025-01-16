using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Application;
using TARA.AuthenticationService.Infrastructure.Data;

namespace TARA.AuthenticationService.Tests.Fixtures;
public class IntegrationTestFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }
    public IntegrationTestFixture()
    {
        var services = new ServiceCollection();

        // Konfiguration laden
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        Configuration = configurationBuilder.Build();


        // Konfiguriere die In-Memory-Datenbank
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
        services.AddDbContext<EventStoreDbContext>(options =>
            options.UseInMemoryDatabase("TestEventStore"));

        // Registriere andere erforderliche Dienste

        services.RegisterApplicationServices(Configuration, true);

        ServiceProvider = services.BuildServiceProvider();

        // Initialisiere die Datenbank
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
        var eventStore = scopedServices.GetRequiredService<EventStoreDbContext>();
        eventStore.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Bereinigen der Ressourcen
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureDeleted();
            var eventStore = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
            eventStore.Database.EnsureDeleted();

            ServiceProvider.Dispose();
        }
    }
}
