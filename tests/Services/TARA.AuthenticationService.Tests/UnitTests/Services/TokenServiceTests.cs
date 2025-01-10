using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TARA.AuthenticationService.Application.Services;

namespace TARA.AuthenticationService.Tests.UnitTests.Services;
public class TokenServiceTests
{
    [Fact]
    public void GenerateToken_Should_Return_Valid_JWT_Token()
    {
        // Arrange
        var userId = "12345";
        var tokenService = new TokenService();
        var secretKey = "TheSuperSecretKeyFromTaraIsAwesome"; // Dies sollte der gleiche Schlüssel wie im TokenService sein
        var issuer = "tara-authservice";
        var audience = "tara";

        // Act
        var token = tokenService.GenerateToken(userId);
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };

        var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);

        // Assert
        validatedToken.Should().NotBeNull();
        validatedToken.Should().BeOfType<JwtSecurityToken>();
        principal.Identity.Should().NotBeNull();
        principal.Identity!.IsAuthenticated.Should().BeTrue();
        principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value.Should().Be(userId);
    }
}
