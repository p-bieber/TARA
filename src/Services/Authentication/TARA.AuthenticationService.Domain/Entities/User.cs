using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private readonly List<object> _uncommittedEvents = [];

#pragma warning disable CS8618 
    private User() { } // ef core
#pragma warning restore CS8618 
    private User(Guid userId, Username name, Password password, Email email)
    {
        Id = userId;
        Username = name;
        Password = password;
        Email = email;

        var @event = new UserCreatedEvent(Id, Username.Value, Email.Value);
        _uncommittedEvents.Add(@event);
        ApplyEvent(@event);
    }

    public static User Create(Username username, Password password, Email email)
    {
        return new(Guid.NewGuid(), username, password, email);
    }

    public void ApplyEvent(object @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                Id = e.UserId;
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
