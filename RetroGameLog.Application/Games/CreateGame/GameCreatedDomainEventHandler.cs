using MediatR;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Domain.Games;
using RetroGameLog.Domain.Games.Events;

namespace RetroGameLog.Application.Games.CreateGame;

internal sealed class GameCreatedDomainEventHandler : INotificationHandler<GameCreatedDomainEvent>
{
    private readonly IGameRepository _gameRepository;

    private readonly INotificationService _notification;

    public GameCreatedDomainEventHandler(IGameRepository gameRepository, INotificationService notification)
    {
        _gameRepository = gameRepository;
        _notification = notification;
    }

    public async Task Handle(GameCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(notification.GameId, cancellationToken);

        if (game == null)
            return;

        var message = $"{game.Title} has been created successfully.";

        await _notification.SendAsync("Success", message);
    }
}
