using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Options;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Tests.UnitTests.Services;
public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly TokenOptions _options;
    public TokenServiceTests()
    {
        var options = Options.Create(new TokenOptions
        {
            Issuer = "tara",
            Audience = "tara",
            SecretKey = "TheSuperSecretKeyFromTaraIsAwesome"
        });
        _options = options.Value;
        _tokenService = new TokenService(options);
    }

    [Fact]
    public void GenerateToken_Should_Return_Valid_JWT_Token()
    {
        // Arrange
        var user = User.Create(Username.Create("TestUser").Value, Password.Create("TestPassword").Value, Email.Create("Testuser@testmail.com").Value);

        // Act
        var tokenResult = _tokenService.GenerateToken(user);
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey))
        };

        var principal = handler.ValidateToken(tokenResult.Value, validationParameters, out var validatedToken);

        // Assert
        tokenResult.IsSuccess.Should().BeTrue();
        validatedToken.Should().NotBeNull();
        validatedToken.Should().BeOfType<JwtSecurityToken>();
        principal.Identity.Should().NotBeNull();
        principal.Identity!.IsAuthenticated.Should().BeTrue();
        principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value.Should().Be(user.UserId.Value.ToString());
    }
}
