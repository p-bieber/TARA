using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;

public record UserChangeEmailDomainEvent(Guid Id, UserId UserId, Email Email) : DomainEvent(Id);