using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Reviews.Events;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(Guid id, Guid gameId, Reviewer reviewer, ReviewContent reviewContent, Rating rating, DateTime createdAt) : base(id)
    {
        GameId = gameId;
        Reviewer = reviewer;
        Content = reviewContent;
        Rating = rating;
        CreatedAt = createdAt;
    }

    private Review() { }

    public Guid GameId { get; private set; }

    public Reviewer Reviewer { get; private set; }

    public ReviewContent Content { get; private set; }

    public Rating Rating { get; private set; }

    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// This factory method creates a new review instance.
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="reviewer"></param>
    /// <param name="reviewContent"></param>
    /// <param name="rating"></param>
    /// <param name="createdAt"></param>
    /// <returns>a new review object!</returns>
    public static Review CreateReview(Guid gameId, Reviewer reviewer, ReviewContent reviewContent, Rating rating, DateTime createdAt)
    {
        var review = new Review(Guid.NewGuid(), gameId, reviewer, reviewContent, rating, createdAt);

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}
