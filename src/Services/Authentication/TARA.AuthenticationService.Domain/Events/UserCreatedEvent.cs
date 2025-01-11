namespace TARA.AuthenticationService.Domain.Events;
public record UserCreatedEvent(Guid UserId, string Username, string Email);
