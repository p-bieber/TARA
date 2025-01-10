namespace TARA.AuthenticationService.Domain.ValueObjects;

public class UserName
{
    public string Value { get; private set; }

    public UserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length < 5)
        {
            throw new ArgumentException("Username must be at least 5 characters long.", nameof(username));
        }

        Value = username;
    }
}