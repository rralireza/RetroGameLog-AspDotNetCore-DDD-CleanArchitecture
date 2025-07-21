using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Reviews;

namespace RetroGameLog.Domain.Achivements;

public sealed class Achivement : Entity
{
    public Achivement(Guid id, Guid gameId, AchivementTitle title, AchivementDescription description, DateTime unlockedAt) : base(id)
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
}
