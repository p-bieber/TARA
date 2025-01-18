using MediatR;
using TARA.Shared.Primitives;

namespace TARA.AuthenticationService.Domain.Primitives;
public record DomainEvent(Guid Id) : IDomainEvent, INotification;
