namespace TARA.AuthenticationService.Infrastructure.Options;
public class PasswordOptions
{
    public const string Section = "PasswordOptions";
    public int WorkFactor { get; set; }
}
