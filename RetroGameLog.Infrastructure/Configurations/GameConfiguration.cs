using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroGameLog.Domain.Games;

namespace RetroGameLog.Infrastructure.Configurations;

internal sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(g => g.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .HasConversion(t => t.Value, value => GameTitle.Create(value));

        builder.Property(p => p.Platform)
            .HasConversion(p => p.PlatformName, value => Platform.FindPlatform(value));

        builder.Property(r => r.ReleaseYear)
            .HasConversion(r => r.Value, value => ReleaseYear.Create(value));

        builder.Property(g => g.Genre)
            .HasConversion(g => g.GenreTitle, value => Genre.FindGenre(value));

        builder.Property(d => d.Developer)
            .HasMaxLength(50)
            .HasConversion(d => d.Value, value => Developer.Create(value));

        builder.Property<uint>("RowVersion").IsRowVersion();
    }
}
