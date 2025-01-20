using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.ValueObjects;

public sealed class Username : ValueObject
{
    public const int MinLength = 5;
    public const int MaxLength = 20;
    public string Value { get; }

    private Username(string username)
    {
        Value = username;
    }

    public static Result<Username> Create(string username) =>
        Result.Create(username)
            .Ensure(x => !string.IsNullOrWhiteSpace(x), UsernameErrors.Empty)
            .Ensure(x => x.Length <= MaxLength, UsernameErrors.TooLong)
            .Ensure(x => x.Length >= MinLength, UsernameErrors.TooShort)
            .Ensure(x => x.All(char.IsLetterOrDigit), UsernameErrors.InvalidCharacters)
            .Map(x => new Username(x));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}