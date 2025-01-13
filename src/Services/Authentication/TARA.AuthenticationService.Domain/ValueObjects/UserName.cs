namespace TARA.AuthenticationService.Domain.ValueObjects;

public class Username
{
    public string Value { get; private set; }

    private Username(string username)
    {
        Value = username;
    }

    public static Username Create(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));
        return new(username);
    }
}