using TARA.AuthenticationService.Domain.Primitives;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Domain.Users.DomainEvents;

public record UserChangeNameDomainEvent(Guid Id, UserId UserId, Username Username) : DomainEvent(Id);
