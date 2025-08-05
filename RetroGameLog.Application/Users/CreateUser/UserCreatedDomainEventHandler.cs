using MediatR;
using RetroGameLog.Application.Abstractions.Notification;
using RetroGameLog.Domain.Users;
using RetroGameLog.Domain.Users.Events;

namespace RetroGameLog.Application.Users.CreateUser;

internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;

    private readonly INotificationService _notificationService;

    public UserCreatedDomainEventHandler(IUserRepository userRepository, INotificationService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user == null)
            return;

        string message = "User has been created successfully.";

        await _notificationService.SendAsync("Success", message);
    }
}