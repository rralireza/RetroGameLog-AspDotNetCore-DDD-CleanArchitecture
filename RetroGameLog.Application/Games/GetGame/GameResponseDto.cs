namespace RetroGameLog.Application.Games.GetGame;

public sealed class GameResponseDto
{
    public Guid Id { get; init; }

    public string Title { get; init; }
    
    public string Platform { get; init; }

    public int ReleaseYear { get; init; }

    public string Genre { get; init; }

    public string Developer { get; init; }
}