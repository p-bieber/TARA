using FluentAssertions;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.ValueObjects;
public class EmailTests
{
    [Fact]
    public void Should_Create_Email_When_Valid_Value()
    {
        var email = Email.Create("test@example.com");
        email.Should().NotBeNull();
        email.Value.Should().BeEquivalentTo("test@example.com");
    }

    [Fact]
    public void Should_Throw_Exception_When_Invalid_Value()
    {
        Action action = () => Email.Create("");
        action.Should().Throw<ArgumentException>();
    }
}
