using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;
public sealed record UserChangeNameDomainEvent(UserId UserId, Username Username)
    : DomainEvent(AggregateId: UserId.Value);
