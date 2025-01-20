using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.Login;

public class LoginQueryHandler(IUserReadRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService) : IQueryHandler<LoginQuery, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var repoResult = await userRepository.GetUserByNameAsync(request.Username, cancellationToken);
        if (repoResult.IsSuccess)
        {
            var isPasswordVerified = passwordHasher.VerifyPassword(request.Password, repoResult.Value.Password.Value);
            if (isPasswordVerified)
            {
                var resultToken = tokenService.GenerateToken(repoResult.Value.Id.ToString());
                if (resultToken.IsSuccess)
                    return new LoginResponse(resultToken.Value);
            }
        }

        return UserErrors.WrongLoginCredientials;
    }
}