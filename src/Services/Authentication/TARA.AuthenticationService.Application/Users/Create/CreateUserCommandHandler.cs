using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.Create;
public class CreateUserCommandHandler(IUserWriteRepository userWriteRepository, IEventStore eventStore)
    : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // validation and user creation
        var username = Username.Create(request.Username);
        var password = Password.Create(request.Password);
        var email = Email.Create(request.Email);

        if (username.IsFailure || password.IsFailure || email.IsFailure)
        {
            return Error.None;
        }

        var user = User.Create(username.Value, password.Value, email.Value);

        var resultRepo = await userWriteRepository.AddUserAsync(user);
        if (resultRepo.IsFailure)
        {
            return resultRepo.Error;
        }
        var eventStoreResult = await eventStore.SaveUncommitedEventsAsync(user);
        if (eventStoreResult.IsFailure)
        {
            return eventStoreResult.Error;
        }

        return Result.Success();

    }
}
