namespace TARA.AuthenticationService.Domain.ValueObjects;

public class Username
{
    public string Value { get; private set; }

#pragma warning disable CS8618 
    private Username() { } // ef core
#pragma warning restore CS8618 
    private Username(string username)
    {
        Value = username;
    }

    public static Username Create(string username)
    {
        return string.IsNullOrEmpty(username)
            ? throw new ArgumentNullException(nameof(username))
            : new(username);
    }
}