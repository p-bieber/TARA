using global::TARA.AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TARA.AuthenticationService.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Authentication");

        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().OwnsOne(u => u.Username).Property(u => u.Value).IsRequired().HasMaxLength(15);
        modelBuilder.Entity<User>().OwnsOne(u => u.Password).Property(u => u.Value).IsRequired();
        modelBuilder.Entity<User>().OwnsOne(u => u.Email).Property(u => u.Value).IsRequired();
    }
}
