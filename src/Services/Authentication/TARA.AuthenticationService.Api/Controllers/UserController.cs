using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Create;
using TARA.AuthenticationService.Application.Users.GetUser;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Api.Controllers;

[Route("[controller]")]
public class UserController(ILogger<UserController> logger, ISender sender) : ApiController(logger, sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        CreateUserCommand command = new(request.Username, request.Password, request.Email);
        Result<UserId> result = await _sender.Send(command, cancellationToken) as Result<UserId>;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetUserById), new { id = result.Value.Value }, result)
            : HandleFailure(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginQuery loginQuery = new(request.Username, request.Password);
        Result<LoginResponse> result = await _sender.Send(loginQuery, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(id);
        Result<UserResponse> result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound();
    }

    [Authorize]
    [HttpGet("byName")]
    public async Task<IActionResult> GetUserByName([FromQuery] string username)
    {
        GetUserByUsernameQuery query = new(username);
        Result<UserResponse> result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound();
    }
}