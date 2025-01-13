using TARA.Shared;

namespace TARA.AuthenticationService.Domain;
public static class AppErrors
{
    public static AppError SaveError =>
        new("App.SaveError", "Error while save entity");
    public static class UserError
    {
        public static AppError NotFound =>
            new("User.NotFound", "User not found");

        public static AppError WrongLoginCredientials =>
            new("User.WrongLoginCredientials", "Wrong login credientials");
    }
}
