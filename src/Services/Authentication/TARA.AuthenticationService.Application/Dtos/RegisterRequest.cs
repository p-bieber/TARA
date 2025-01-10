namespace TARA.AuthenticationService.Application.Dtos;

public record RegisterRequest(string UserName, string Password, string Email);