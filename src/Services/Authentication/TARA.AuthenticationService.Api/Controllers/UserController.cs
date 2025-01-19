using MediatR;
using Microsoft.AspNetCore.Mvc;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Create;
using TARA.AuthenticationService.Application.Users.GetUser;
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
            ? CreatedAtAction(nameof(GetUserById), new { userId = result.Value.Value }, result)
            : HandleFailure(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserById([FromQuery] string userId, CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(Guid.Parse(userId));
        Result<UserResponse> result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetUserByName([FromQuery] string username)
    {
        GetUserByUsernameQuery query = new(username);
        Result<UserResponse> result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }
}