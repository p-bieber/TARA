using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Api.Controllers;

[Route("[controller]")]
public class AuthController(ILogger<AuthController> logger, ISender sender) : ApiController(logger, sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginQuery loginQuery = new(request.Username, request.Password);
        Result<LoginResponse> result = await _sender.Send(loginQuery, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }

}
