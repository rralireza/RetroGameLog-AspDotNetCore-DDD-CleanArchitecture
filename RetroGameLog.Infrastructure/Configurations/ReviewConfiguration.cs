using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroGameLog.Domain.Games;
using RetroGameLog.Domain.Reviews;

namespace RetroGameLog.Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Reviewer)
            .HasMaxLength(40)
            .HasConversion(r => r.Username, username => Reviewer.SetReviewer(username))
            .IsRequired();

        builder
            .Property(x => x.Content)
            .HasMaxLength(500)
            .HasConversion(c => c.Value, value => ReviewContent.CreateContent(value))
            .IsRequired();

        builder
            .Property(x => x.Rating)
            .HasConversion(r => r.Score, score => Rating.SetReviewScore(score))
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .HasOne<Game>()
            .WithMany()
            .HasForeignKey(x => x.GameId);

        builder.Property<uint>("RowVersion").IsRowVersion();
    }
}
