using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Infrastructure.Services;
public class EventStore(EventStoreDbContext context) : IEventStore
{
    public async Task<Result> SaveEventAsync<T>(Guid streamId, T @event) where T : IDomainEvent
    {
        try
        {
            Event newEvent = new()
            {
                EventId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                StreamId = streamId,
                Type = @event.GetType().Name,
                Data = JsonSerializer.Serialize(@event)
            };
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("EventStore.SaveEventAsync", ex.Message));
        }
    }

    public async Task<Result<IEnumerable<T?>>> GetEventsAsync<T>(Guid streamId) where T : IDomainEvent
    {
        try
        {
            var events = await context.Events
                .Where(e => e.StreamId == streamId)
                .OrderBy(e => e.CreatedAt)
                .ToListAsync();
            return events.Select(e => JsonSerializer.Deserialize<T>(e.Data)).ToList();
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<T?>>(new Error("EventStore.GetEventsAsync", ex.Message));
        }
    }

    public async Task<Result> SaveUncommitedEventsAsync(AggregateRoot aggregate)
    {
        try
        {
            var events = aggregate.GetDomainEvents();
            aggregate.ClearDomainEvents();
            foreach (var @event in events)
            {
                await SaveEventAsync(aggregate.Id, @event);
            }
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("EventStore.SaveUncommitedEventsAsync", ex.Message));
        }
    }
}

