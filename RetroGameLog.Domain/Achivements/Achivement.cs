using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Achivements.Events;
using RetroGameLog.Domain.Reviews;

namespace RetroGameLog.Domain.Achivements;

public sealed class Achivement : Entity
{
    private Achivement(Guid id, Guid gameId, AchivementTitle title, AchivementDescription description, DateTime unlockedAt) : base(id)
    {
        GameId = gameId;
        Title = title;
        Description = description;
        UnlockedAt = unlockedAt;
    }

    public Guid GameId { get; private set; }

    public AchivementTitle Title { get; private set; }

    public AchivementDescription Description { get; private set; }

    public DateTime UnlockedAt { get; private set; }

    public static Achivement CreateAchivement(Guid gameId, AchivementTitle title, AchivementDescription description, DateTime unlockedAt)
    {
        if (gameId == null) throw new ArgumentNullException("Game can't be empty!");

        if (title == null) throw new ArgumentNullException($"{nameof(title)} can't be blank!");

        if (description == null) throw new ArgumentNullException($"{nameof(description)} can't be blank!");

        if (unlockedAt == null) throw new ArgumentNullException($"{nameof(unlockedAt)} can't be blank!");

        var achivement = new Achivement(Guid.NewGuid(), gameId, title, description, unlockedAt);

        achivement.RaiseDomainEvent(new AchivementCreatedDomainEvent(achivement.Id));

        return achivement;
    }
}
