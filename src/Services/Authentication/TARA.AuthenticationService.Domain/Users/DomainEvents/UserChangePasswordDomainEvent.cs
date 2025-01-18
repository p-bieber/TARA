using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;
public sealed record UserChangePasswordDomainEvent(UserId UserId, Password Password)
    : DomainEvent(AggregateId: UserId.Value);
