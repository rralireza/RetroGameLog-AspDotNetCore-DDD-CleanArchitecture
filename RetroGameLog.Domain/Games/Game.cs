using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games.Events;

namespace RetroGameLog.Domain.Games;

public sealed class Game : Entity
{
    private Game(Guid id,
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

    public static Game CreateGame(GameTitle title, Platform platform, ReleaseYear releaseYear, Genre genre, Developer developer)
    {
        if (title == null) throw new ArgumentNullException($"{nameof(title)} can't be blank!");

        if (platform == null) throw new ArgumentNullException($"{nameof(platform)} can't be blank!");

        if (releaseYear == null) throw new ArgumentNullException($"{nameof(releaseYear)} can't be blank!");

        if (genre == null) throw new ArgumentNullException($"{nameof(genre)} can't be blank!");

        if (developer == null) throw new ArgumentNullException($"{nameof(developer)} can't be blank!");

        var game = new Game(Guid.NewGuid(), title, platform, releaseYear, genre, developer);

        game.RaiseDomainEvent(new GameCreatedDomainEvent(game.Id));

        return game;
    }
}
