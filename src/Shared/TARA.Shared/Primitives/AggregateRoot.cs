namespace TARA.Shared.Primitives;
public abstract class AggregateRoot(Guid id) : Entity(id)
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void ApplyEvents(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event);
        }
    }
    public abstract void ApplyEvent(IDomainEvent @event);

}
