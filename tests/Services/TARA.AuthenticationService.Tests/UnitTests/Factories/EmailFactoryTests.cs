using FluentAssertions;
using FluentValidation;
using TARA.AuthenticationService.Application.Factories;

namespace TARA.AuthenticationService.Tests.UnitTests.Factories;
public class EmailFactoryTests
{
    private readonly EmailFactory _emailFactory = new();

    [Fact]
    public void Should_Create_Valid_Email()
    {
        // Arrange
        var address = "test@example.com";

        // Act
        var email = _emailFactory.Create(address);

        // Assert
        email.Value.Should().Be(address);
    }

    [Fact]
    public void Should_Throw_Exception_For_Null_Email()
    {
        // Arrange
        string? address = null;

        // Act
#pragma warning disable CS8604 // Mögliches Nullverweisargument.
        Action act = () => _emailFactory.Create(address);
#pragma warning restore CS8604 // Mögliches Nullverweisargument.

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Should_Throw_Exception_For_Empty_Email()
    {
        // Arrange
        var address = "";

        // Act
        Action act = () => _emailFactory.Create(address);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Should_Throw_Exception_For_Whitespace_Email()
    {
        // Arrange
        var address = "   ";

        // Act
        Action act = () => _emailFactory.Create(address);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Email_Format()
    {
        // Arrange
        var address = "invalid-email";

        // Act
        Action act = () => _emailFactory.Create(address);

        // Assert
        act.Should().Throw<ValidationException>();
    }
}
