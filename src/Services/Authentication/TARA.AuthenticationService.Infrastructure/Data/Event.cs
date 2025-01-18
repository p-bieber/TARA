namespace TARA.AuthenticationService.Infrastructure.Data;
public class Event
{
    public Guid EventId { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public Guid StreamId { get; init; }
    public string Type { get; init; }
    public string Data { get; init; }
}
