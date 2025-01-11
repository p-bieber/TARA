using Microsoft.EntityFrameworkCore;

namespace TARA.AuthenticationService.Infrastructure.Data;

public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
}
