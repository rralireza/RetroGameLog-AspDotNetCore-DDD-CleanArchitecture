using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Application.Exceptions;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Games;

namespace RetroGameLog.Application.Games.CreateGame;

internal sealed class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, Guid>
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateGameCommandHandler(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        if (await _gameRepository.IsGameTitleExistsAsync(request.Title))
            return Result.Failure<Guid>(GameErrors.DuplicateTitle);

        try
        {
            var game = Game.CreateGame(
                GameTitle.Create(request.Title),
                Platform.FindPlatform(request.Platform),
                ReleaseYear.Create(request.ReleaseYear),
                Genre.FindGenre(request.Genre),
                Developer.Create(request.Developer)
                );

            _gameRepository.Add(game);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(game.Id);
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(GameErrors.DuplicateTitle);
        }
    }
}
