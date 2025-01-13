using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Application.CQRS.Features.Login;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain;
using TARA.Shared;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;
public class AuthControllerTests
{
    private readonly Mock<ILogger<AuthController>> _loggerMock;
    private readonly Mock<ISender> _senderMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _loggerMock = new Mock<ILogger<AuthController>>();
        _senderMock = new Mock<ISender>();
        _authController = new AuthController(_loggerMock.Object, _senderMock.Object);
    }

    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var request = new LoginQuery("Maxim", "password");

        _senderMock.Setup(s => s.Send(request, CancellationToken.None)).ReturnsAsync(Result.Success(new LoginResponseDto("valid-token")));

        var result = await _authController.Login(request) as OkObjectResult;

        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(200);
        result?.Value.Should().BeEquivalentTo(new { Token = "valid-token" });
    }

    [Fact]
    public async Task Login_Should_Return_Unauthorized_When_Credentials_Are_Invalid()
    {
        var request = new LoginQuery("Maxim", "password");

        _senderMock.Setup(s => s.Send(request, CancellationToken.None)).ReturnsAsync(Result.Failure<LoginResponseDto>(AppErrors.UserError.WrongLoginCredientials));

        var result = await _authController.Login(request) as BadRequestObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
        result.Value.Should().BeEquivalentTo(AppErrors.UserError.WrongLoginCredientials);
    }
}
