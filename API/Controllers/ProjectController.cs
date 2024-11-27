using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpPost("CreateProject")]
    public async Task<ActionResult> CreateProject([FromBody] ProjectCreateModel model)
    {
        int projectId = projectService.CreateProject(model);
        if (projectId == 0)
            return BadRequest("error al crear el team");
        return Ok(projectId);
    }
    [HttpPost("GetUserTeamProjects")]
    public async Task<ActionResult> GetUserTeamProjects([FromBody] ProjectListModel model)
    {
        return Ok(projectService.GetProjectsFromTeams(model));
    }
}