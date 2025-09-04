namespace RetroGameLog.Domain.Games;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsGameTitleExistsAsync(string title);

    void Add(Game game);
}
