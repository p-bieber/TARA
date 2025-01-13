using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Application.CQRS.Features.Login;

namespace TARA.AuthenticationService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(ILogger<AuthController> logger, ISender sender) : BaseController(logger, sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        var result = await _sender.Send(request);
        if (result == null)
            return BadRequest();

        if (result.IsSuccess)
            return Ok(result.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Unauthorized();
    }
}
