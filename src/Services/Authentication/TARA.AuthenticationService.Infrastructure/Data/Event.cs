namespace TARA.AuthenticationService.Infrastructure.Data;
public class Event
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

