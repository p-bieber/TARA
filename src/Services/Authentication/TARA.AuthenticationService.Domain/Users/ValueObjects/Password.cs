using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; }

    private Password(string password)
    {
        Value = password;
    }

    public static Result<Password> Create(string password)
    {
        return Result.Create(password)
            .Ensure(x => !string.IsNullOrWhiteSpace(x), PasswordErrors.Empty)
            .Map(x => new Password(x));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}