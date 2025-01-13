using FluentAssertions;
using FluentValidation;
using TARA.AuthenticationService.Application.Factories;

namespace TARA.AuthenticationService.Tests.UnitTests.Factories;

public class UserNameFactoryTests
{
    private readonly UserNameFactory _userNameFactory = new();

    [Fact]
    public void Should_Create_Valid_UserName()
    {
        // Arrange
        var value = "ValidUser123";

        // Act
        var userName = _userNameFactory.Create(value);

        // Assert
        userName.Value.Should().Be(value);
    }

    [Fact]
    public void Should_Throw_Exception_For_Short_UserName()
    {
        // Arrange
        var value = "abc";

        // Act
        Action act = () => _userNameFactory.Create(value);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Username must consist of at least 5 characters");
    }

    [Fact]
    public void Should_Throw_Exception_For_Long_UserName()
    {
        // Arrange
        var value = "thisusernameiswaytoolong";

        // Act
        Action act = () => _userNameFactory.Create(value);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Username is too long. maximum 15 characters allowed");
    }

    [Fact]
    public void Should_Throw_Exception_For_UserName_With_Special_Characters()
    {
        // Arrange
        var value = "invalid@user";

        // Act
        Action act = () => _userNameFactory.Create(value);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Username must not contain any special characters");
    }
}

