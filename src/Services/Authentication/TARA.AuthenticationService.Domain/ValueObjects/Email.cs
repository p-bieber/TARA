namespace TARA.AuthenticationService.Domain.ValueObjects;
public class Email
{
    public string Value { get; private set; }

#pragma warning disable CS8618 
    private Email() { } // ef core
#pragma warning restore CS8618 
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentNullException(nameof(value))
            : new Email(value);
    }
}