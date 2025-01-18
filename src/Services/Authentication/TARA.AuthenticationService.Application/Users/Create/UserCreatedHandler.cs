using MediatR;
using TARA.AuthenticationService.Domain.Users.DomainEvents;

namespace TARA.AuthenticationService.Application.Users.Create;
public sealed class UserCreatedHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}
