using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.Errors;

public static class PasswordErrors
{
    public static Error Empty =>
        new("Password.Empty", "Password is empty");
    public static Error TooShort =>
        new("Password.TooShort", "Password is too short");
    public static Error NoDigit =>
        new("Password.NoDigit", "Password must contain at least one digit");
    public static Error NoUpper =>
        new("Password.NoUpper", "Password must contain at least one upper case letter");
    public static Error NoLower =>
        new("Password.NoLower", "Password must contain at least one lower case letter");
    public static Error NoSymbol =>
        new("Password.NoSymbol", "Password must contain at least one symbol");
}