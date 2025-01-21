using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TARA.AuthenticationService.Infrastructure.Options;

public class TokenOptionsSetup(IConfiguration configuration) : IConfigureOptions<TokenOptions>
{
    public void Configure(TokenOptions options)
    {
        configuration.GetSection(TokenOptions.Section).Bind(options);
    }
}
