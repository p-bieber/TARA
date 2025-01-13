using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Application.Dtos;

namespace TARA.AuthenticationService.Application.CQRS.Features.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IQuery<UserDto>;
