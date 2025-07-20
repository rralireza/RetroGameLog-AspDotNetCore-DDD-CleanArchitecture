using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Reviews;

public sealed class Review : Entity
{
    public Review(Guid id, Guid gameId, Reviewer reviewer, ReviewContent reviewContent, Rating rating, DateTime createdAt) : base(id)
    {
        GameId = gameId;
        Reviewer = reviewer;
        Content = reviewContent;
        Rating = rating;
        CreatedAt = createdAt;
    }

    public Guid GameId { get; private set; }

    public Reviewer Reviewer { get; private set; }

    public ReviewContent Content { get; private set; }

    public Rating Rating { get; private set; }

    public DateTime CreatedAt { get; private set; }
}
