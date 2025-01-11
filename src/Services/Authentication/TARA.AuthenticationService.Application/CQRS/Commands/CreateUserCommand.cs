using MediatR;

namespace TARA.AuthenticationService.Application.CQRS.Commands;

public record CreateUserCommand(string Username, string Password, string Email) : IRequest<bool>;
