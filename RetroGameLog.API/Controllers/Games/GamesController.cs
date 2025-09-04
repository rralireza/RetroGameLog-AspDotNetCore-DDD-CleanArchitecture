using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetroGameLog.Application.Games.GetAllGames;

namespace RetroGameLog.API.Controllers.Games;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly ISender _sender;

    public GamesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("GetAllGames")]
    public async Task<IActionResult> GetAllGames(CancellationToken cancellationToken)
    {
        var query = new GetAllGamesQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
