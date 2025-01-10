namespace TARA.AuthenticationService.Domain.ValueObjects;

public class Password
{
    public string Value { get; private set; }

    public Password(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
        {
            throw new ArgumentException("Password must be at least 5 characters long.", nameof(password));
        }

        Value = password;
    }
}