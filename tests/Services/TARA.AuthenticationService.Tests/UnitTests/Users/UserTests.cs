using FluentAssertions;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.DomainEvents;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Tests.UnitTests.Users;
public class UserTests
{
    private readonly Username _username;
    private readonly Password _password;
    private readonly Email _email;

    public UserTests()
    {
        _username = Username.Create("testuser").Value;
        _password = Password.Create("testpassword").Value;
        _email = Email.Create("test@email.com").Value;
    }

    [Fact]
    public void Should_Create_User()
    {

        var user = User.Create(_username, _password, _email);
        user.Should().NotBeNull();
        user.Should().BeOfType<User>();
        user.Username.Should().Be(_username);
        user.Email.Should().Be(_email);
        user.Password.Should().Be(_password);
    }

    [Fact]
    public void Create_Should_Raise_UserCreateddomainEvent()
    {
        var user = User.Create(_username, _password, _email);
        var events = user.GetDomainEvents();
        events.Should().NotBeNull();
        events.Should().HaveCount(1);
        events.First().Should().BeOfType<UserCreatedDomainEvent>();
    }
    [Fact]
    public void Should_Update_Username()
    {
        var user = User.Create(_username, _password, _email);
        var newUsername = Username.Create("newusername").Value;
        user.UpdateName(newUsername);
        user.Username.Should().Be(newUsername);
    }
    [Fact]
    public void UpdateName_Should_Raise_UsernameUpdatedDomainEvent()
    {
        var user = User.Create(_username, _password, _email);
        var newUsername = Username.Create("newusername").Value;
        user.UpdateName(newUsername);
        var events = user.GetDomainEvents();
        events.Should().NotBeNull();
        events.Should().HaveCount(2);
        events.Last().Should().BeOfType<UserChangeNameDomainEvent>();
    }
    [Fact]
    public void Should_Update_Email()
    {
        var user = User.Create(_username, _password, _email);
        var newEmail = Email.Create("newtest@email.com").Value;
        user.UpdateEmail(newEmail);
        user.Email.Should().Be(newEmail);
    }
    [Fact]
    public void UpdateEmail_Should_Raise_EmailUpdatedDomainEvent()
    {
        var user = User.Create(_username, _password, _email);
        var newEmail = Email.Create("newtest@email.com").Value;
        user.UpdateEmail(newEmail);
        var events = user.GetDomainEvents();
        events.Should().NotBeNull();
        events.Should().HaveCount(2);
        events.Last().Should().BeOfType<UserChangeEmailDomainEvent>();
    }
    [Fact]
    public void Should_Update_Password()
    {
        var user = User.Create(_username, _password, _email);
        var newPassword = Password.Create("newpassword").Value;
        user.UpdatePassword(newPassword);
        user.Password.Should().Be(newPassword);
    }
    [Fact]
    public void UpdatePassword_Should_Raise_PasswordUpdatedDomainEvent()
    {
        var user = User.Create(_username, _password, _email);
        var newPassword = Password.Create("newpassword").Value;
        user.UpdatePassword(newPassword);
        var events = user.GetDomainEvents();
        events.Should().NotBeNull();
        events.Should().HaveCount(2);
        events.Last().Should().BeOfType<UserChangePasswordDomainEvent>();
    }
    [Fact]
    public void Should_Apply_Event()
    {
        var user = User.Create(_username, _password, _email);
        var newUsername = Username.Create("newusername").Value;
        var newEmail = Email.Create("newtest@email.com").Value;
        var newPassword = Password.Create("newpassword").Value;
        user.ApplyEvent(new UserChangeNameDomainEvent(user.UserId, newUsername));
        user.ApplyEvent(new UserChangeEmailDomainEvent(user.UserId, newEmail));
        user.ApplyEvent(new UserChangePasswordDomainEvent(user.UserId, newPassword));
        user.Username.Should().Be(newUsername);
        user.Email.Should().Be(newEmail);
        user.Password.Should().Be(newPassword);
    }
    [Fact]
    public void ApplyEvent_Should_Apply_UserCreatedDomainEvent()
    {
        var user = User.Create(_username, _password, _email);
        var newUsername = Username.Create("newusername").Value;
        var newEmail = Email.Create("newtest@email.com").Value;
        var newPassword = Password.Create("newpassword").Value;
        user.ApplyEvent(new UserCreatedDomainEvent(user.UserId, newUsername, newPassword, newEmail));
        user.Username.Should().Be(newUsername);
        user.Email.Should().Be(newEmail);
    }


}
