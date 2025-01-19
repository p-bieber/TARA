using TARA.AuthenticationService.Application.Abstractions;

namespace TARA.AuthenticationService.Application.Users.GetUser;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
