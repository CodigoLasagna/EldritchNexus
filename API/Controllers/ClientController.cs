using Business.Contracts;
using Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FastSurvey.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController(IClientService clientService) : ControllerBase
{

    [HttpPost("ConnectToServer")]
    public async Task<ActionResult> ConnectToServer([FromBody] string serverUrl)
    {
        var result = await clientService.ConnectToServer(serverUrl); // Llamada asíncrona
        return Ok(result);
    }

    [HttpPost("SendMessage")]
    public ActionResult SendMessage()
    {
        var result = clientService.SendMessageToServer();
        return Ok(result);
    }
}
