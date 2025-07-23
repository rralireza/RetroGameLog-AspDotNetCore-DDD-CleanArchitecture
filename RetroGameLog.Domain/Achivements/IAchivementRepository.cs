namespace RetroGameLog.Domain.Achivements;

public interface IAchivementRepository
{
    Task<Achivement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Achivement achivement);
}
