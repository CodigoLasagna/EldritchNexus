using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController(IOrganizationService organizationService) : ControllerBase
{
    [HttpPost("GetOrganizationsPerType")]
    public async Task<ActionResult> RegiserUser([FromBody] OrganizationPerTypeModel model)
    {
        return Ok(organizationService.GetOrganizations(model));
    }
}