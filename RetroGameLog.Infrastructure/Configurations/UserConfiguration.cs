using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroGameLog.Domain.Achivements;
using RetroGameLog.Domain.Reviews;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.FullName, fullName =>
        {
            fullName
            .Property(x => x.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(50)
            .IsRequired();

            fullName
            .Property(x => x.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(50)
            .IsRequired();
        });

        builder
            .Property(x => x.Email)
            .HasMaxLength(100)
            .HasColumnName("Email")
            .HasConversion(e => e.Value, value => new Email(value))
            .IsRequired();

        builder
            .Property(x => x.Username)
            .HasColumnName("Username")
            .HasMaxLength(50)
            .HasConversion(u => u.Value, value => new Username(value))
            .IsRequired();

        builder
            .Property(x => x.RegisteredAt)
            .HasColumnName("RegisteredAt")
            .IsRequired();

        builder
            .Property(x => x.RowVersion)
            .IsConcurrencyToken()
            .IsRowVersion();

        //builder.Property<uint>("RowVersion").IsRowVersion();

        //builder
        //    .Property(x => x.RowVersion)
        //    .HasColumnType("timestamp")
        //    .IsConcurrencyToken()
        //    .ValueGeneratedOnAddOrUpdate();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasIndex(x => x.IdentityId).IsUnique();
    }
}
