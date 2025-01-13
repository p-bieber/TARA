using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.Shared;

namespace TARA.AuthenticationService.Application.CQRS.Features.CreateUser;
public class CreateUserCommandHandler(IUserService userService) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateUser(request.Username, request.Password, request.Email);
        return result;
    }
}
