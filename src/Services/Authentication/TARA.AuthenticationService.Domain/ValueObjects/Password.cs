namespace TARA.AuthenticationService.Domain.ValueObjects;

public class Password
{
    public string Value { get; private set; }

#pragma warning disable CS8618 
    private Password() { } // ef core
#pragma warning restore CS8618 
    private Password(string password)
    {
        Value = password;
    }

    public static Password Create(string password)
    {
        return string.IsNullOrEmpty(password)
            ? throw new ArgumentNullException(nameof(password))
            : new Password(password);
    }
}