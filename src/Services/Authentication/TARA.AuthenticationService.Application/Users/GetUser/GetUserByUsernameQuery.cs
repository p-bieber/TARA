using TARA.AuthenticationService.Application.Abstractions;

namespace TARA.AuthenticationService.Application.Users.GetUser;

public record GetUserByUsernameQuery(string Username) : IQuery<UserResponse>;
