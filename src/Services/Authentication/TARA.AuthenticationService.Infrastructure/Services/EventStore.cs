using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;

namespace TARA.AuthenticationService.Infrastructure.Services;
public class EventStore : IEventStore
{
    private readonly EventStoreDbContext _context;

    public EventStore(EventStoreDbContext context)
    {
        _context = context;
    }

    public async Task SaveEventAsync<T>(T @event) where T : class
    {
        var eventType = @event.GetType().Name;
        var data = JsonSerializer.Serialize(@event);
        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Type = eventType,
            Data = data,
            CreatedAt = DateTimeOffset.UtcNow
        };
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T?>> GetEventsAsync<T>(Guid aggregateId) where T : class
    {
        var events = await _context.Events
            .Where(e => e.Id == aggregateId)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();

        return events.Select(e => JsonSerializer.Deserialize<T>(e.Data));
    }
}

