using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGamesRental.Api.Core;
using VideoGamesRental.Application.VideoGames.DeleteVideoGame;
using VideoGamesRental.Application.VideoGames.GetVideoGameById;
using VideoGamesRental.Application.VideoGames.GetVideoGames;
using VideoGamesRental.Application.VideoGames.SaveVideoGame;
using VideoGamesRental.Domain.VideoGames;

namespace VideoGamesRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController(
    ISender inMediator,
    IValidator<SaveVideoGameRequest> inSaveVideoGameRequestValidator,
    IValidator<DeleteVideoGameRequest> inDeleteVideoGameRequestValidator
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
    
    [HttpPost]
    public async Task<IActionResult> SaveVideoGameAsync([FromBody] VideoGame inVideoGame)
    {
        var request = new SaveVideoGameRequest(inVideoGame);
        var validationResult = await inSaveVideoGameRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ValidationFailureMessage());
        }
        
        await inMediator.Send(request.Adapt<SaveVideoGameCommand>());
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("{inId:guid}")]
    public async Task<IActionResult> DeleteVideoGameAsync(Guid inId)
    {
        var request = new DeleteVideoGameRequest(inId);
        var validationResult = await inDeleteVideoGameRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ValidationFailureMessage());
        }
        
        await inMediator.Send(request.Adapt<DeleteVideoGameCommand>());
        
        return Ok();
    }
}