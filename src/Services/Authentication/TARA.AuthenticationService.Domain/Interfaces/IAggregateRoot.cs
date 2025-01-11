namespace TARA.AuthenticationService.Domain.Interfaces;

public interface IAggregateRoot
{
    void ApplyEvent(object @event);
    IEnumerable<object> GetUncommittedEvents();
    void ClearUncommittedEvents();
}
