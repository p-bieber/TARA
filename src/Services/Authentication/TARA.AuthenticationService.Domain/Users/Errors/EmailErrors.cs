using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.Errors;

public static class EmailErrors
{
    public static Error Empty =>
        new("Email.Empty", "Email is empty");
    public static Error TooLong =>
        new("Email.TooLong", "Email is too long");
    public static Error InvalidFormat =>
        new("Email.InvalidFormat", "Email is invalid format");
}