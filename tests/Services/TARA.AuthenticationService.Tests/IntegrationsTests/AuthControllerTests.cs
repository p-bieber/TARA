using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Application.Dtos;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;
public class AuthControllerTests
{
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _tokenServiceMock = new Mock<ITokenService>();
        _userServiceMock = new Mock<IUserService>();
        _authController = new AuthController(_tokenServiceMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var user = User.Create("Maxim", "password", "maxim@mail.de");
        var request = new LoginRequest("Maxim", "password");

        _userServiceMock.Setup(s => s.ValidateUserAsync("Maxim", "password"))
            .ReturnsAsync((true, user.Id));
        _userServiceMock.Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenServiceMock.Setup(s => s.GenerateToken(user.Id.Value.ToString()))
            .Returns("valid-token");

        var result = await _authController.Login(request) as OkObjectResult;

        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(200);
        result?.Value.Should().BeEquivalentTo(new { Token = "valid-token" });
    }

    [Fact]
    public void Login_Should_Return_Unauthorized_When_Credentials_Are_Invalid()
    {

    }
}
