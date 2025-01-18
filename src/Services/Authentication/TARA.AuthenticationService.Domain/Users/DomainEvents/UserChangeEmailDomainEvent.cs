using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;
public sealed record UserChangeEmailDomainEvent(UserId UserId, Email Email)
    : DomainEvent(AggregateId: UserId.Value);