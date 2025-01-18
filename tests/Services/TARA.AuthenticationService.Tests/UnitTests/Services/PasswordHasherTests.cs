using FluentAssertions;
using Microsoft.Extensions.Options;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.AuthenticationService.Infrastructure.Settings;

namespace TARA.AuthenticationService.Tests.UnitTests.Services;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherTests()
    {
        var options = Options.Create(new PasswordSettings { WorkFactor = 12 });
        _passwordHasher = new PasswordHasher(options);
    }

    [Fact]
    public void HashPassword_Should_Return_NonEmpty_String()
    {
        // Arrange
        var password = "TestPassword";

        // Act
        var hashedPassword = _passwordHasher.HashPassword(password);

        // Assert
        hashedPassword.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void VerifyPassword_Should_Return_True_For_Correct_Password()
    {
        // Arrange
        var password = "TestPassword";
        var hashedPassword = _passwordHasher.HashPassword(password);

        // Act
        var result = _passwordHasher.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_Should_Return_False_For_Incorrect_Password()
    {
        // Arrange
        var password = "TestPassword";
        var hashedPassword = _passwordHasher.HashPassword(password);
        var wrongPassword = "WrongPassword";

        // Act
        var result = _passwordHasher.VerifyPassword(wrongPassword, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }
}
