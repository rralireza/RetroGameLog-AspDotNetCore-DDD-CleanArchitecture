namespace RetroGameLog.Domain.Games;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Game game);
}
