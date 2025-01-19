using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Infrastructure.Configuration;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
               .HasConversion(
                   u => u.Value,
                   u => UserId.Create(u).Value);

        builder.Property(u => u.Username)
                .HasConversion(
                     u => u.Value,
                     u => Username.Create(u).Value)
                .IsRequired()
                .HasMaxLength(Username.MaxLength);

        builder.Property(u => u.Password)
                .HasConversion(
                     u => u.Value,
                     u => Password.Create(u).Value)
                .IsRequired();

        builder.Property(u => u.Email)
                .HasConversion(
                     u => u.Value,
                     u => Email.Create(u).Value)
                .IsRequired()
                .HasMaxLength(Email.MaxLength);

        builder.HasIndex(u => u.Username)
               .IsUnique();
        builder.HasIndex(u => u.Email)
               .IsUnique();
    }
}
