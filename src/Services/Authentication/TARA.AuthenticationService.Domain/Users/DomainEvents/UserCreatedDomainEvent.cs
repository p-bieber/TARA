using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;
public sealed record UserCreatedDomainEvent(UserId UserId, Username Username, Password Password, Email Email)
    : DomainEvent(AggregateId: UserId.Value);
