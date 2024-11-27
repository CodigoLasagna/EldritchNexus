using Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController(IOrganizationService organizationService) : ControllerBase
{
    
}