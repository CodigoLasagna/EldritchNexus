using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("RegisterUser")]
    public async Task<ActionResult> RegiserUser([FromBody] UserCreateModel model)
    {
        int userId = userService.CreateUser(model);
        if (userId == 0) return BadRequest("Error al registar usuario");
        if (userId == -1) return BadRequest("Correo ya registrado");
        return Ok(userId);
    }
    [HttpPost("LoginUser")]
    public async Task<ActionResult> LoginUser([FromBody] UserLoginModel model)
    {
        string response = userService.LoginUser(model);
        if (response == "0") return BadRequest("Correo o Contraseña erroneos");
        return Ok(response);
    }
}