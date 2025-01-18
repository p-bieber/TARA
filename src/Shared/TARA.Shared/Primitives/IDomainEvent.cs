namespace TARA.Shared.Primitives;
public interface IDomainEvent
{
    Guid Id { get; }
    Guid AggregateId { get; }
}
