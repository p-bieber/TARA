using MediatR;
using TARA.Shared.Primitives;

namespace TARA.AuthenticationService.Domain.Primitives;
public record DomainEvent(Guid AggregateId) : IDomainEvent, INotification
{
    public Guid Id { get; } = Guid.NewGuid();
}
