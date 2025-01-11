using MediatR;
using TARA.AuthenticationService.Application.CQRS.Queries;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Application.CQRS.Handlers;
public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByNameAsync(request.Username);
    }
}
