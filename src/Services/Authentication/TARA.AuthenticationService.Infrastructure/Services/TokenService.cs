using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Infrastructure.Options;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Services;
public class TokenService(IOptions<TokenOptions> options) : ITokenService
{
    private readonly TokenOptions _options = options.Value;

    public Result<string> GenerateToken(User user)
    {
        try
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        catch (Exception ex)
        {
            return Result.Failure<string>(new("Error.GenerateToken", ex.Message));
        }
    }
}
