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

        // Load configuration
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        Configuration = configurationBuilder.Build();


        // Configure the in-memory database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        // Register other required services

        services.RegisterApplicationServices(Configuration, true);

        ServiceProvider = services.BuildServiceProvider();

        // Initialize the database
        using var scope = ServiceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
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
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureDeleted();

            ServiceProvider.Dispose();
        }
    }
}
