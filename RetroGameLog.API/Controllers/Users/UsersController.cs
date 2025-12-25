using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroGameLog.Application.Users.CreateUser;
using RetroGameLog.Application.Users.LoginUser;

namespace RetroGameLog.API.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(nameof(CreateUser))]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Username, request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost(nameof(LoginUser))]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser(LoginUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
