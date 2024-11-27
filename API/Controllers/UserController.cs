using Business.Contracts;
using Helper.Security;
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
    [HttpPost("UpdateUser")]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateModel model)
    {
        return Ok(userService.UpdateUserData(model));
    }
    [HttpPost("UpdateUserRole")]
    public async Task<ActionResult> UpdateUserRole([FromBody] UserUpdateRoleModel model)
    {
        return Ok(userService.UpdateRole(model));
    }
    
    [HttpPost("RenewToken")]
    public async Task<ActionResult> RenewToken([FromBody] RenewTokenModel model)
    {
        try
        {
            // Obtener el ID del usuario a partir del token actual
            var TokenManager = new TokenManager();
            int userId = TokenManager.ValidateToken(model.Token);
            if (userId == -1) return BadRequest("Token inválido");
    
            // Renovar el token con los datos actualizados del usuario
            string newToken = userService.RenewUserToken(model.Token, userId);
            return Ok(new { Token = newToken });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}