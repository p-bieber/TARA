using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Domain.Entities;
public class User : IAggregateRoot
{
    public UserId Id { get; private set; }
    public UserName UserName { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    private List<object> _uncommittedEvents = new();

    private User(UserId userId, UserName name, Password password, Email email)
    {
        Id = userId;
        UserName = name;
        Password = password;
        Email = email;

        var @event = new UserCreatedEvent(Id.Value, UserName.Value, Email.Value);
        _uncommittedEvents.Add(@event);
        ApplyEvent(@event);
    }

    public static User Create(UserName username, Password password, Email email)
    {
        return new(UserId.Create(), username, password, email);
    }

    public void ApplyEvent(object @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                Id = UserId.Create(e.UserId);
                UserName = UserName.Create(e.Username);
                Email = Email.Create(e.Email);
                break;
            default:
                break;
        }
    }

    public IEnumerable<object> GetUncommittedEvents() => _uncommittedEvents;
    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
}
