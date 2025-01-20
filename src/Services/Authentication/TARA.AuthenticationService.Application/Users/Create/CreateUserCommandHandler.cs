using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.Create;
public sealed class CreateUserCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<Result<UserId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await IsUsernameExist(request.Username, cancellationToken))
            return UsernameErrors.AlreadyExists;
        if (await IsEmailExist(request.Email, cancellationToken))
            return EmailErrors.AlreadyExists;


        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(passwordHasher.HashPassword(request.Password));
        var emailResult = Email.Create(request.Email);

        var user = User.Create(usernameResult.Value, passwordResult.Value, emailResult.Value);

        await userWriteRepository.AddUserAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.UserId;
    }

    private async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken)
    {
        var result = await userReadRepository.GetUserByEmailAsync(email, cancellationToken);
        return result.IsSuccess;
    }
    private async Task<bool> IsUsernameExist(string username, CancellationToken cancellationToken)
    {
        var result = await userReadRepository.GetUserByNameAsync(username, cancellationToken);
        return result.IsSuccess;
    }
}
