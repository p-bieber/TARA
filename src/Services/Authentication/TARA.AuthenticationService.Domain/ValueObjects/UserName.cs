namespace TARA.AuthenticationService.Domain.ValueObjects;

public class UserName
{
    public string Value { get; private set; }

    private UserName(string username)
    {
        Value = username;
    }

    public static UserName Create(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));
        return new(username);
    }
}