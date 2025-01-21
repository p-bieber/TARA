using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TARA.AuthenticationService.Infrastructure.Options;

public class PasswordOptionsSetup(IConfiguration configuration) : IConfigureOptions<PasswordOptions>
{
    public void Configure(PasswordOptions options)
    {
        configuration.GetSection(PasswordOptions.Section).Bind(options);
    }
}