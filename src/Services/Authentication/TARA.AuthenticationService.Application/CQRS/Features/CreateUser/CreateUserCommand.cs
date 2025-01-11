using TARA.AuthenticationService.Application.CQRS.Abstractions;

namespace TARA.AuthenticationService.Application.CQRS.Features.CreateUser;

public record CreateUserCommand(string Username, string Password, string Email) : ICommand;
