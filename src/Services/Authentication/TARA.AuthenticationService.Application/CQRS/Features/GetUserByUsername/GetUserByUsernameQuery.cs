using TARA.AuthenticationService.Application.CQRS.Abstractions;
using TARA.AuthenticationService.Domain.Entities;

namespace TARA.AuthenticationService.Application.CQRS.Features.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IQuery<User>;
