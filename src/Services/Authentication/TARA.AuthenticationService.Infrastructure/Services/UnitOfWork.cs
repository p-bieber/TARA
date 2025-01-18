using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.Shared.Primitives;

namespace TARA.AuthenticationService.Infrastructure.Services;
internal sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertAndSaveDomainEvents();
        UpdateAuditableEntities();

        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertAndSaveDomainEvents()
    {
        var events = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new Event
            {
                EventId = domainEvent.Id,
                CreatedAt = DateTimeOffset.UtcNow,
                AggregateId = domainEvent.AggregateId,
                Type = domainEvent.GetType().Name,
                Data = JsonSerializer.Serialize(domainEvent)
            })
            .ToList();

        dbContext.Events.AddRange(events);
    }

    private void UpdateAuditableEntities()
    {
        var entries = dbContext.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }

        }
    }
}
