using FluentAssertions;
using FluentValidation;
using Moq;
using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Tests.UnitTests.Factories;

public class PasswordFactoryTests
{
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly PasswordFactory _passwordFactory;

    public PasswordFactoryTests()
    {
        _passwordFactory = new(_passwordHasherMock.Object);
    }

    [Fact]
    public void Should_Create_Valid_Hashed_Password()
    {
        // Arrange
        var password = "ValidPass123!";
        var hashedPassword = "HashedPassword";

        _passwordHasherMock.Setup(s => s.HashPassword(password)).Returns(hashedPassword);

        // Act
        var result = _passwordFactory.Create(password);

        // Assert
        result.Value.Should().NotBeNullOrWhiteSpace();
        result.Value.Should().Be(hashedPassword);
    }

    [Fact]
    public void Should_Throw_Exception_For_Short_Password()
    {
        // Arrange
        var password = "Short1!";

        // Act
        Action act = () => _passwordFactory.Create(password);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Password must consist of at least 8 characters");
    }

    [Fact]
    public void Should_Throw_Exception_For_Long_Password()
    {
        // Arrange
        var password = new string('A', 26) + "1!";

        // Act
        Action act = () => _passwordFactory.Create(password);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Password is too long. maximum 25 characters allowed");
    }

    [Fact]
    public void Should_Throw_Exception_For_Missing_Special_Character()
    {
        // Arrange
        var password = "Password123";

        // Act
        Action act = () => _passwordFactory.Create(password);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Password must contain at least one special character");
    }

    [Fact]
    public void Should_Throw_Exception_For_Missing_Uppercase()
    {
        // Arrange
        var password = "password123!";

        // Act
        Action act = () => _passwordFactory.Create(password);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Password must contain at least one uppercase letter");
    }

    [Fact]
    public void Should_Throw_Exception_For_Missing_Number()
    {
        // Arrange
        var password = "Password!";

        // Act
        Action act = () => _passwordFactory.Create(password);

        // Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Password must contain at least one number");
    }
}
