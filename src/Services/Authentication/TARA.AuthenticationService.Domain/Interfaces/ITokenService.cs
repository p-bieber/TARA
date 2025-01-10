namespace TARA.AuthenticationService.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(string userId);
}