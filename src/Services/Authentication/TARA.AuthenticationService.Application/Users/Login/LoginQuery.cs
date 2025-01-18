using TARA.AuthenticationService.Application.Abstractions;

namespace TARA.AuthenticationService.Application.Users.Login;
public record LoginQuery(string Username, string Password) : IQuery<LoginResponse>;
