using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Application.Users.GetUser;

public sealed class GetUserByIdQueryHandler(IUserReadRepository userRepository) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var repoResult = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (repoResult.IsFailure)
            return repoResult.Error;
        var userDto = new UserResponse(repoResult.Value.Id.ToString(), repoResult.Value.Username.Value, repoResult.Value.Email.Value);
        return userDto;
    }
}
