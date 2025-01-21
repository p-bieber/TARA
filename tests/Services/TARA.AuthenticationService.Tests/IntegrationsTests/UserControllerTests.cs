using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;
public class UserControllerTests
{
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly Mock<ISender> _senderMock;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _loggerMock = new Mock<ILogger<UserController>>();
        _senderMock = new Mock<ISender>();
        _userController = new UserController(_loggerMock.Object, _senderMock.Object);
    }

    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var request = new LoginRequest("Maxim", "password");
        var query = new LoginQuery(request.Username, request.Password);

        _senderMock.Setup(s => s.Send(query, CancellationToken.None)).ReturnsAsync(Result.Success(new LoginResponse("valid-token")));

        var result = await _userController.Login(request, CancellationToken.None) as OkObjectResult;

        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(200);
        result?.Value.Should().BeEquivalentTo(new { Token = "valid-token" });
    }

    [Fact]
    public async Task Login_Should_Return_Unauthorized_When_Credentials_Are_Invalid()
    {
        var request = new LoginRequest("Maxim", "password");
        var query = new LoginQuery(request.Username, request.Password);

        _senderMock.Setup(s => s.Send(query, CancellationToken.None)).ReturnsAsync(Result.Failure<LoginResponse>(UserErrors.WrongLoginCredientials));

        var result = await _userController.Login(request, CancellationToken.None) as BadRequestObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        var problemDetails = result.Value as ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Detail.Should().Be(UserErrors.WrongLoginCredientials.Message);
    }
}
