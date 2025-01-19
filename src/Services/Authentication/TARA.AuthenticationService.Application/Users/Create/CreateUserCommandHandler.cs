using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.Create;
public class CreateUserCommandHandler(IUserWriteRepository userWriteRepository, IUnitOfWork unitOfWork)
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
            return new Error("CreateUser.ValidationError", "Validation not succeeded");
        }

        var user = User.Create(username.Value, password.Value, email.Value);

        await userWriteRepository.AddUserAsync(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
