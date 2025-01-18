using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.ValueObjects;
public class Email : ValueObject
{
    public const int MaxLength = 320;
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        return Result.Create(value)
            .Ensure(x => !string.IsNullOrWhiteSpace(x), EmailErrors.Empty)
            .Ensure(x => x.Length <= MaxLength, EmailErrors.TooLong)
            .Ensure(x => x.Split('@').Length == 2, EmailErrors.InvalidFormat)
            .Map(x => new Email(x));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}