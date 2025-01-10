namespace TARA.AuthenticationService.Domain.ValueObjects;

public class Password
{
    public string Value { get; private set; }

    private Password(string password)
    {
        Value = password;
    }

    public static Password Create(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        return new Password(password);
    }
}