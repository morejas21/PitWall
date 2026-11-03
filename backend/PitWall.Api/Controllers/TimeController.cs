using Microsoft.AspNetCore.Mvc;
using PitWall.Api.Contracts;

namespace PitWall.Api.Controllers;

[ApiController]
[Route("api/time")]
public class TimeController(TimeProvider timeProvider) : ControllerBase
{
    [HttpGet]
    public ServerTimeResponse Get() => new(timeProvider.GetUtcNow());
}
