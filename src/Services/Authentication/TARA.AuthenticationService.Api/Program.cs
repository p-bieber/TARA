using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TARA.AuthenticationService.Application;
using TARA.AuthenticationService.Infrastructure;
using TARA.AuthenticationService.Infrastructure.Data;

namespace TARA.AuthenticationService.Api;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterInfrastructureServices(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                var secretKey = builder.Configuration["TokenOptions:SecretKey"];
                var issuer = builder.Configuration["TokenOptions:Issuer"];
                var audience = builder.Configuration["TokenOptions:Audience"];

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    throw new InvalidOperationException("TokenOptions are not configured properly");
                }

                o.RequireHttpsMetadata = false;

                if (builder.Environment.IsDevelopment())
                {
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Authentication failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token validated for: " + context.Principal?.Identity?.Name);
                            return Task.CompletedTask;
                        }
                    };
                }

                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            DatabaseMigrate(app);
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();
        app.Run();
    }


    private static void DatabaseMigrate(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}