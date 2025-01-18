using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.GetUser;
public class GetUserByUsernameQueryHandler(IUserReadRepository userRepository) : IQueryHandler<GetUserByUsernameQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var repoResult = await userRepository.GetUserByNameAsync(request.Username);
        if (repoResult.IsFailure)
            return Result.Failure<UserResponse>(repoResult.Error);

        var userDto = new UserResponse(repoResult.Value.Id.ToString(), repoResult.Value.Username.Value, repoResult.Value.Email.Value);
        return Result.Success(userDto);
    }
}
