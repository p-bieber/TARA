namespace TARA.AuthenticationService.Domain.ValueObjects;
public class Email
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        return new Email(value);
    }
}