using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Features.GetUserByUsername;
public class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByNameAsync(request.Username);
        if (user == null)
        {
            return Result.Failure<User>(AppErrors.UserNotFound);
        }
        return Result.Success<User>(user);
    }
}
