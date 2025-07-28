using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games;

namespace RetroGameLog.Application.Games.CreateGame;

internal sealed class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, Guid>
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateGameCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Result<Guid>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        if (await _gameRepository.IsGameTitleExistsAsync(request.Title))
            return Result.Failure<Guid>(GameErrors.DuplicateTitle);

        var game = Game.CreateGame(request.Title, request.Platform, request.ReleaseYear, request.Genre, request.Developer);

        _gameRepository.Add(game);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(game.Id);
    }
}
