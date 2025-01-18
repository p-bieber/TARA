using FluentAssertions;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.ValueObjects;
public class EmailTests
{
    [Fact]
    public void Should_Create_Email_When_Valid_Value()
    {
        var emailResult = Email.Create("test@example.com");
        emailResult.Should().NotBeNull();
        emailResult.IsSuccess.Should().BeTrue();
        emailResult.Value.Should().BeOfType<Email>();
        emailResult.Value.Value.Should().BeEquivalentTo("test@example.com");
    }

    [Fact]
    public void Should_Throw_Exception_When_Value_Is_Empty()
    {
        var emailResult = Email.Create("");
        emailResult.Should().NotBeNull();
        emailResult.IsFailure.Should().BeTrue();
        emailResult.Error.Should().Be(EmailErrors.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_When_Value_Is_TooLong()
    {
        var emailResult = Email.Create(string.Concat(new string('t', Email.MaxLength), "@example.com"));
        emailResult.Should().NotBeNull();
        emailResult.IsFailure.Should().BeTrue();
        emailResult.Error.Should().Be(EmailErrors.TooLong);
    }

    [Fact]
    public void Should_Throw_Exception_When_Value_Is_Invalid_Format()
    {
        var emailResult = Email.Create("testexample.com");
        emailResult.Should().NotBeNull();
        emailResult.IsFailure.Should().BeTrue();
        emailResult.Error.Should().Be(EmailErrors.InvalidFormat);
    }
}
