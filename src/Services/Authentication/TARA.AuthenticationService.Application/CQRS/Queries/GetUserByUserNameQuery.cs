using MediatR;
using TARA.AuthenticationService.Domain.Entities;

namespace TARA.AuthenticationService.Application.CQRS.Queries;

public class GetUserByUsernameQuery : IRequest<User?>
{
    public string Username { get; }

    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }
}
