using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Infrastructure.Configuration;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.OwnsOne(u => u.Username)
               .Property(u => u.Value)
               .IsRequired()
               .HasMaxLength(15);
        builder.OwnsOne(u => u.Password)
               .Property(u => u.Value)
               .IsRequired();
        builder.OwnsOne(u => u.Email)
               .Property(u => u.Value)
               .IsRequired();

        // Mapping
        builder.Property(u => u.UserId)
               .HasConversion(
                   u => u.Value,
                   u => UserId.Create(u).Value);
        builder.Property(u => u.Username)
                .HasConversion(
                     u => u.Value,
                     u => Username.Create(u).Value);
        builder.Property(u => u.Password)
                .HasConversion(
                     u => u.Value,
                     u => Password.Create(u).Value);
        builder.Property(u => u.Email)
                .HasConversion(
                     u => u.Value,
                     u => Email.Create(u).Value);

    }
}
