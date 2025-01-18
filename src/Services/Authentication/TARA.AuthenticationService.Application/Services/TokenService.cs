using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Services;
public class TokenService : ITokenService
{
    public Result<string> GenerateToken(string userId)
    {
        try
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TheSuperSecretKeyFromTaraIsAwesome")); // TODO
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tara-authservice",
                audience: "tara",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        catch (Exception ex)
        {
            return Result.Failure<string>(new("Error.GenerateToken", ex.Message));
        }
    }
}
