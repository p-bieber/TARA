using TARA.AuthenticationService.Domain.Users.DomainEvents;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.Shared.Primitives;

namespace TARA.AuthenticationService.Domain.Users;
public sealed class User : AggregateRoot
{
    public UserId UserId { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private User(
        UserId userId,
        Username username,
        Password password,
        Email email) : base(userId.Value)
    {
        UserId = userId;
        Username = username;
        Password = password;
        Email = email;

        RaiseDomainEvent(new UserCreatedDomainEvent(userId, username, password, email));
    }

    public static User Create(Username username, Password password, Email email)
    {
        var userId = UserId.Create().Value;
        return new(userId, username, password, email);
    }


    public void UpdateName(Username username)
    {
        Username = username;
        RaiseDomainEvent(new UserChangeNameDomainEvent(UserId, Username));
    }
    public void UpdateEmail(Email email)
    {
        Email = email;
        RaiseDomainEvent(new UserChangeEmailDomainEvent(UserId, Email));
    }

    public void UpdatePassword(Password password)
    {
        Password = password;
        RaiseDomainEvent(new UserChangePasswordDomainEvent(UserId, Password));
    }

    public override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserCreatedDomainEvent e:
                Username = e.Username;
                Email = e.Email;
                break;
            case UserChangeNameDomainEvent e:
                Username = e.Username;
                break;
            case UserChangeEmailDomainEvent e:
                Email = e.Email;
                break;
            case UserChangePasswordDomainEvent e:
                Password = e.Password;
                break;
            default:
                break;
        }
    }
}
