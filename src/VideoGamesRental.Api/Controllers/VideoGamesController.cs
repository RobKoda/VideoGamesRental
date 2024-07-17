using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGamesRental.Application.VideoGames.GetVideoGameById;
using VideoGamesRental.Application.VideoGames.GetVideoGames;

namespace VideoGamesRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController(
    ISender inMediator
    ) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetVideoGamesAsync() => 
        Ok((await inMediator.Send(new GetVideoGamesQuery())).VideoGames);
    
    [HttpGet]
    [Route("{inId:guid}")]
    public async Task<IActionResult> GetVideoGameByIdAsync(Guid inId) => 
        (await inMediator.Send(new GetVideoGameByIdQuery(inId)))
        .VideoGame
        .Match(Ok, (IActionResult) NotFound());
}