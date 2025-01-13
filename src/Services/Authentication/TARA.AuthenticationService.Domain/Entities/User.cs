using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User : IAggregateRoot
{
    public UserId Id { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private readonly List<object> _uncommittedEvents = [];

    private User(UserId userId, Username name, Password password, Email email)
    {
        Id = userId;
        Username = name;
        Password = password;
        Email = email;

        var @event = new UserCreatedEvent(Id.Value, Username.Value, Email.Value);
        _uncommittedEvents.Add(@event);
        ApplyEvent(@event);
    }

    public static User Create(Username username, Password password, Email email)
    {
        return new(UserId.Create(), username, password, email);
    }

    public void ApplyEvent(object @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                Id = UserId.Create(e.UserId);
                Username = Username.Create(e.Username);
                Email = Email.Create(e.Email);
                break;
            default:
                break;
        }
    }

    public IEnumerable<object> GetUncommittedEvents() => _uncommittedEvents;
    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
}
