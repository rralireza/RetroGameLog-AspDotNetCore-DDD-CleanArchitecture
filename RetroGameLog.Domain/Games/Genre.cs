namespace RetroGameLog.Domain.Games;

public sealed record Genre
{
    internal static readonly Genre Unknown = new("Unknown");
    public static readonly Genre Action = new("Action");
    public static readonly Genre Adventure = new("Adventure");
    public static readonly Genre RolePlaying = new("Role Playing");
    public static readonly Genre Simulation = new("Simulation");
    public static readonly Genre Strategy = new("Strategy");
    public static readonly Genre Sports = new("Sports");
    public static readonly Genre Racing = new("Racing");
    public static readonly Genre Puzzle = new("Puzzle");
    public static readonly Genre Fighting = new("Fighting");
    public static readonly Genre Shooter = new("Shooter");
    public static readonly Genre Horror = new("Horror");
    public static readonly Genre Platformer = new("Platformer");
    public static readonly Genre Rhythm = new("Rhythm");
    public static readonly Genre Educational = new("Educational");

    private Genre(string title) => GenreTitle = title;

    public string GenreTitle { get; init; }

    public static Genre FindGenre(string title) => AllGenres.FirstOrDefault(x => x.GenreTitle == title)
        ?? throw new ApplicationException("Invalid genre title!");

    public static readonly IReadOnlyCollection<Genre> AllGenres = new List<Genre>
    {
        Unknown,
        Action,
        Adventure,
        RolePlaying,
        Simulation,
        Strategy,
        Sports,
        Racing,
        Puzzle,
        Fighting,
        Shooter,
        Horror,
        Platformer,
        Rhythm,
        Educational
    }.AsReadOnly();

    public override string ToString() => GenreTitle;
}
