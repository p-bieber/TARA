using MediatR;

namespace TARA.AuthenticationService.Application.CQRS.Commands;

public class CreateUserCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public CreateUserCommand(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}
