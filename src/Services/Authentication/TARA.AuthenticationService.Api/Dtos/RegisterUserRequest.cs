namespace TARA.AuthenticationService.Api.Dtos;

public record RegisterUserRequest(string Username, string Password, string Email);
