using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.Errors;

public static class UserErrors
{
    public static Error NotFound =>
        new("User.NotFound", "User not found");

    public static Error WrongLoginCredientials =>
        new("User.WrongLoginCredientials", "Wrong login credientials");
}
