using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroGameLog.Application.Users.CreateUser;

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

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Username, request.Password, request.RegisteredAt);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
