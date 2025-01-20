using FluentAssertions;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.Users.ValueObjects;
public class UsernameTests
{
    [Fact]
    public void Should_Create_Username_When_Valid_Value()
    {
        var usernameResult = Username.Create("testuser");
        usernameResult.Should().NotBeNull();
        usernameResult.IsSuccess.Should().BeTrue();
        usernameResult.Value.Should().BeOfType<Username>();
        usernameResult.Value.Value.Should().BeEquivalentTo("testuser");
    }
    [Fact]
    public void Should_Create_Error_When_Value_Is_Empty()
    {
        var usernameResult = Username.Create("");
        usernameResult.Should().NotBeNull();
        usernameResult.IsFailure.Should().BeTrue();
        usernameResult.Error.Should().Be(UsernameErrors.Empty);
    }
    [Fact]
    public void Should_Create_Error_When_Value_Is_TooLong()
    {
        var usernameResult = Username.Create(new string('t', Username.MaxLength + 1));
        usernameResult.Should().NotBeNull();
        usernameResult.IsFailure.Should().BeTrue();
        usernameResult.Error.Should().Be(UsernameErrors.TooLong);
    }
    [Fact]
    public void Should_Create_Error_When_Value_Is_TooShort()
    {
        var usernameResult = Username.Create(new string('t', Username.MinLength - 1));
        usernameResult.Should().NotBeNull();
        usernameResult.IsFailure.Should().BeTrue();
        usernameResult.Error.Should().Be(UsernameErrors.TooShort);
    }
    [Theory]
    [InlineData("test@")]
    [InlineData("test!")]
    [InlineData("test#")]
    [InlineData("test$")]
    [InlineData("test%")]
    [InlineData("test^")]
    [InlineData("test&")]
    [InlineData("test*")]
    [InlineData("test(")]
    [InlineData("test)")]
    [InlineData("test_")]
    [InlineData("test-")]

    public void Should_Create_Error_When_Value_Contains_Invalid_Characters(string value)
    {
        var usernameResult = Username.Create(value);
        usernameResult.Should().NotBeNull();
        usernameResult.IsFailure.Should().BeTrue();
        usernameResult.Error.Should().Be(UsernameErrors.InvalidCharacters);
    }
}
