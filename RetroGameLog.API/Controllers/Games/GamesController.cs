using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetroGameLog.Application.Games.CreateGame;
using RetroGameLog.Application.Games.GetAllGames;
using RetroGameLog.Application.Games.GetGame;

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

    [HttpGet("GetGame/{id}")]
    public async Task<IActionResult> GetGame(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetGameQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost("CreateGame")]
    public async Task<IActionResult> CreateGame(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var query = new CreateGameCommand(request.Title, request.Platform, request.ReleaseYear, request.Genre, request.Developer);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetGame), new { id = result.Value }, result.Value);
    }
}
