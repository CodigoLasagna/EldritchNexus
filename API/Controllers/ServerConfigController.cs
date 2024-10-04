using Business.Contracts;
using Data.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerConfigController(IServerConfigService serverConfigService) : ControllerBase
{
    [HttpGet("ScanNetwork")]
    public async Task<ActionResult> ScanNetwork()
    {
        var servers = serverConfigService.ScanLocalNetwork();
        return Ok(servers);
    }
}
