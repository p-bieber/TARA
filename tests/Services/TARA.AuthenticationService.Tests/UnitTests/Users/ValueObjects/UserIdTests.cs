using FluentAssertions;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.Users.ValueObjects;
public class UserIdTests
{
    [Fact]
    public void Should_Create_UserId()
    {
        var userIdResult = UserId.Create();
        userIdResult.Should().NotBeNull();
        userIdResult.IsSuccess.Should().BeTrue();
        userIdResult.Value.Should().BeOfType<UserId>();
    }

    [Fact]
    public void Should_Create_UserId_When_Value_Is_Valid()
    {
        var guid = Guid.NewGuid();
        var userIdResult = UserId.Create(guid);
        userIdResult.Should().NotBeNull();
        userIdResult.IsSuccess.Should().BeTrue();
        userIdResult.Value.Should().BeOfType<UserId>();
        userIdResult.Value.Value.Should().Be(guid);
    }
}
