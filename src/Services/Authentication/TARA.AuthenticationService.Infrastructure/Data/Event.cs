namespace TARA.AuthenticationService.Infrastructure.Data;
public class Event
{
    public Guid EventId { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public Guid AggregateId { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Data { get; init; } = string.Empty;
}
