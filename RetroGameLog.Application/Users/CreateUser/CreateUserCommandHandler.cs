using RetroGameLog.Application.Abstractions.Authentication;
using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Domain.Users;

namespace RetroGameLog.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticationService _authenticationService;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            return Result.Failure<Guid>(UserErrors.BlankFullName);

        var fullName = FullName.Create(request.FirstName, request.LastName);

        var email = new Email(request.Email);

        var username = new Username(request.Username);

        var user = User.Create(fullName, email, username);

        var identityId = await _authenticationService.RegisterAsync(user, request.Password, cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}