using global::TARA.AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TARA.AuthenticationService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.UserName.Value).IsRequired().HasMaxLength(15);
        modelBuilder.Entity<User>().Property(u => u.Password.Value).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Email.Value).IsRequired();
    }
}
