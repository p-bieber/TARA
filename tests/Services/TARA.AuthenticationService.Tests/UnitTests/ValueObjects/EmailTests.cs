using FluentAssertions;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.ValueObjects;
public class EmailTests
{
    [Fact]
    public void Should_Create_Email_When_Valid_Value()
    {
        var email = new Email("test@example.com");
        email.Should().NotBeNull();
        email.Value.Should().BeEquivalentTo("test@example.com");
    }

    [Fact]
    public void Should_Throw_Exception_When_Invalid_Value()
    {
        Action action = () => new Email("");
        action.Should().Throw<ArgumentException>();
    }
}
