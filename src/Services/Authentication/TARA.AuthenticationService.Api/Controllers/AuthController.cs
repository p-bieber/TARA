using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController(ILogger<AuthController> logger, ITokenService tokenService, IUserService userService)
    {
        _logger = logger;
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation($"Login called. {request.UserName}");
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
