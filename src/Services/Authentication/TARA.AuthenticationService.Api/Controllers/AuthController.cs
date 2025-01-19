using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Api.Controllers;

[Route("[controller]")]
public class AuthController(ILogger<AuthController> logger, ISender sender) : ApiController(logger, sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery request, CancellationToken cancellationToken)
    {
        Result<LoginResponse> result = await _sender.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }

}
