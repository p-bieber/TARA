using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Features.Login;

public class LoginQueryHandler(IUserService userService, ITokenService tokenService) : IQueryHandler<LoginQuery, LoginResponseDto>
{
    public async Task<Result<LoginResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var resultVerify = await userService.VerifyUserPasswordAsync(request.Username, request.Password);
        if (resultVerify.IsFailure)
            return Result.Failure<LoginResponseDto>(resultVerify.Error);

        var resultToken = tokenService.GenerateToken(resultVerify.Value.Id.Value.ToString());

        return resultToken.IsSuccess
            ? new LoginResponseDto(resultToken.Value)
            : Result.Failure<LoginResponseDto>(resultToken.Error);
    }
}