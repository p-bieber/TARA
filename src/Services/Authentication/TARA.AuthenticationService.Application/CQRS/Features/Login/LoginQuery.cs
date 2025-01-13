using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Application.Dtos;

namespace TARA.AuthenticationService.Application.CQRS.Features.Login;
public record LoginQuery(string Username, string Password) : IQuery<LoginResponseDto>;
