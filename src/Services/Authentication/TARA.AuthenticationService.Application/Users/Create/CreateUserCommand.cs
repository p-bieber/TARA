using TARA.AuthenticationService.Application.Abstractions;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Application.Users.Create;

public sealed record CreateUserCommand(string Username, string Password, string Email) : ICommand<UserId>;
