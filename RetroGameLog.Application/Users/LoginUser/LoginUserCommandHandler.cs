using RetroGameLog.Application.Abstractions.Authentication;
using RetroGameLog.Application.Abstractions.Messaging;
using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var generateToken = await _jwtService.GenerateTokenAsync(request.Email, request.Password, cancellationToken);

        if (generateToken.IsFailure)
            return Result.Failure<AccessTokenResponse>(Error.BadRequest);

        var result = new AccessTokenResponse(generateToken.Value);

        return result;
    }
}
