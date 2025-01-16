using Microsoft.EntityFrameworkCore;
using TARA.AuthenticationService.Application;
using TARA.AuthenticationService.Infrastructure.Data;

namespace TARA.AuthenticationService.Api;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.RegisterApplicationServices(builder.Configuration);
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            DatabaseMigrate(app);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void DatabaseMigrate(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        var eventStore = services.GetRequiredService<EventStoreDbContext>();
        context.Database.Migrate();
        eventStore.Database.Migrate();
    }
}