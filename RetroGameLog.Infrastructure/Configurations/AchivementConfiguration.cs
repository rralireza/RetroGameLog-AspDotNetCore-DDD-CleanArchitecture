using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroGameLog.Domain.Achivements;

namespace RetroGameLog.Infrastructure.Configurations;

internal sealed class AchivementConfiguration : IEntityTypeConfiguration<Achivement>
{
    public void Configure(EntityTypeBuilder<Achivement> builder)
    {
        builder.ToTable("Achivements");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .HasMaxLength(100)
            .HasConversion(t => t.Value, value => AchivementTitle.Create(value))
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(500)
            .HasConversion(d => d.Value, value => AchivementDescription.Create(value))
            .IsRequired();

        builder.Property(x => x.UnlockedAt);
    }
}
