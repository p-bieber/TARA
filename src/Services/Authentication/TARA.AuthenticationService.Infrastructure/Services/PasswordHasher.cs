using Microsoft.Extensions.Options;
using TARA.AuthenticationService.Infrastructure.Settings;

namespace TARA.AuthenticationService.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private readonly int _workFactor;

    public PasswordHasher(IOptions<PasswordSettings> options)
    {
        _workFactor = options.Value.WorkFactor;
    }

    // Hashes the password with a random salt
    public string HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(_workFactor);
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    // Verifies the password against the hashed password
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}

