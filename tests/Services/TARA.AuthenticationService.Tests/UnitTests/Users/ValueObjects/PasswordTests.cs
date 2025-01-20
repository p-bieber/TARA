using FluentAssertions;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.Users.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void Should_Create_Password_When_Valid_Value()
    {
        var passwordResult = Password.Create("testpassword");
        passwordResult.Should().NotBeNull();
        passwordResult.IsSuccess.Should().BeTrue();
        passwordResult.Value.Should().BeOfType<Password>();
        passwordResult.Value.Value.Should().BeEquivalentTo("testpassword");
    }

    [Fact]
    public void Should_Create_Error_When_Value_Is_Empty()
    {
        var passwordResult = Password.Create("");
        passwordResult.Should().NotBeNull();
        passwordResult.IsFailure.Should().BeTrue();
        passwordResult.Error.Should().Be(PasswordErrors.Empty);
    }

}
