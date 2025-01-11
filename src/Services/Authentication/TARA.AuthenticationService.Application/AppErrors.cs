using TARA.Shared;

namespace TARA.AuthenticationService.Application;
public static class AppErrors
{
    public static AppError UserNotFound =>
        new("User.NotFound", "User not found");
}
