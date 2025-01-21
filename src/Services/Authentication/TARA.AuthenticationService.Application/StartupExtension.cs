using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TARA.AuthenticationService.Application.Behaviors;

namespace TARA.AuthenticationService.Application;
public static class StartupExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(ApplicationAssemblyReference.Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}
