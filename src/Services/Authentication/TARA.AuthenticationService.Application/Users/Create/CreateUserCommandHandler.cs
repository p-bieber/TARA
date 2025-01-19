using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.Create;
public sealed class CreateUserCommandHandler(IUserWriteRepository userWriteRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<Result<UserId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(passwordHasher.HashPassword(request.Password));
        var emailResult = Email.Create(request.Email);

        var user = User.Create(usernameResult.Value, passwordResult.Value, emailResult.Value);

        await userWriteRepository.AddUserAsync(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.UserId;
    }
}
