using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Infrastructure.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Permissions);

        builder.HasData([Permission.UserCanRead, Permission.UserCanReadAndWrite]);
    }
}
