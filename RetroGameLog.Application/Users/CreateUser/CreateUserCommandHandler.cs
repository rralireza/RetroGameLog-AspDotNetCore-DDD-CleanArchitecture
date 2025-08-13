using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            return Result.Failure<Guid>(UserErrors.BlankFullName);

        var fullName = FullName.Create(request.FirstName, request.LastName);

        var user = User.Create(fullName, request.Email, request.Username, request.RegisteredAt);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}