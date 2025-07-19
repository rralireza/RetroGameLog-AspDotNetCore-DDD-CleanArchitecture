using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Games;

public sealed class Game : Entity
{
    public Game(Guid id,
                GameTitle title,
                Platform platform,
                ReleaseYear releaseYear,
                Genre genre,
                Developer developer) : base(id)
    {
        Title = title;
        Platform = platform;
        ReleaseYear = releaseYear;
        Genre = genre;
        Developer = developer;
    }

    public GameTitle Title { get; private set; }

    public Platform Platform { get; private set; }

    public ReleaseYear ReleaseYear { get; private set; }

    public Genre Genre { get; private set; }

    public Developer Developer { get; private set; }
}
