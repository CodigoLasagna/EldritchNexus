using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    [HttpPost("CreateTeamInOrganization")]
    public async Task<ActionResult> CreateTeamInOrganization([FromBody] TeamCreateModel model)
    {
        int teamId = teamService.CreateTeamInOrganization(model);
        if (teamId == -1)
            return BadRequest("error al crear el team");
        return Ok(teamId);
    }
    [HttpPost("GetTeamsInUserOrgs")]
    public async Task<ActionResult> GetTeamsInUserOrgs([FromBody] TeamGetListModel model)
    {
        return Ok(teamService.GetListOfTeamsByType(model));
    }
    [HttpPut("LogicRemoveTeam")]
    public async Task<ActionResult> LogicRemoveTeam([FromBody] TeamDelModel model)
    {
        return Ok(teamService.TeamDelete(model));
    }
}