namespace TARA.AuthenticationService.Infrastructure.Options;

public class TokenOptions
{
    public const string Section = "TokenOptions";

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}
