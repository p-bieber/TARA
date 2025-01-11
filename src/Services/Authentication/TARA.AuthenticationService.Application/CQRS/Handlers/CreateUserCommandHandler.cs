using MediatR;
using TARA.AuthenticationService.Application.CQRS.Commands;
using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Domain.Events;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Application.CQRS.Handlers;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventStore _eventStore;
    private readonly UserFactory _userFactory;
    private readonly PasswordFactory _passwordFactory;

    public CreateUserCommandHandler(IUserRepository userRepository, IEventStore eventStore, UserFactory userFactory, PasswordFactory passwordFactory)
    {
        _userRepository = userRepository;
        _eventStore = eventStore;
        _userFactory = userFactory;
        _passwordFactory = passwordFactory;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = _passwordFactory.Create(request.Password);
        var user = _userFactory.Create(request.Username, password.Value, request.Email);

        await _userRepository.AddUserAsync(user);

        var userCreatedEvent = new UserCreatedEvent(user.Id.Value, user.UserName.Value, user.Email.Value);
        await _eventStore.SaveEventAsync(userCreatedEvent);

        return true;
    }
}
