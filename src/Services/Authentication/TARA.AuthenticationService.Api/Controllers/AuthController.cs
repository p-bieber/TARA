using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Application.CQRS.Features.CreateUser;
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

        return result != null
            ? result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error)
            : Conflict();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand request)
    {
        var result = await _sender.Send(request);
        return result == null
            ? Conflict()
            : result.IsSuccess
            ? Ok()
            : result.IsFailure
            ? BadRequest(result.Error)
            : BadRequest();
    }
}
