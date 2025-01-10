using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Anmeldeinformationen validieren
        var (isValid, userId) = await _userService.ValidateUserAsync(request.UserName, request.Password);
        if (isValid && userId != null)
        {
            var token = _tokenService.GenerateToken(userId.Value.ToString());
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
