using TARA.AuthenticationService.Application.Abstractions;

namespace TARA.AuthenticationService.Application.Users.Create;

public record CreateUserCommand(string Username, string Password, string Email) : ICommand;
