using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Features.GetUserByUsername;
public class GetUserByUsernameQueryHandler(IUserService userService) : IQueryHandler<GetUserByUsernameQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetUserByNameAsync(request.Username);
        if (result.IsFailure)
            return Result.Failure<UserDto>(result.Error);

        var userDto = new UserDto(result.Value.Id.ToString(), result.Value.Username.Value, result.Value.Email.Value);
        return Result.Success(userDto);
    }
}
