using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;

public record UserChangePasswordDomainEvent(Guid Id, UserId UserId, Password Password) : DomainEvent(Id);
