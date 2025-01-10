namespace TARA.AuthenticationService.Infrastructure.Configuration;
public class PasswordSettings
{
    public const string Section = "PasswordSettings";
    public int WorkFactor { get; set; }
}
