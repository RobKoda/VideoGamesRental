using Microsoft.AspNetCore.Mvc;

namespace VideoGamesRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}