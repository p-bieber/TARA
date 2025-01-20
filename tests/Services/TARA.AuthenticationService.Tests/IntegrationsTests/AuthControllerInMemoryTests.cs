using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.AuthenticationService.Tests.Fixtures;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;

public class AuthControllerInMemoryTests : IAsyncLifetime, IClassFixture<IntegrationTestFixture>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly AuthController _authController;
    private readonly IPasswordHasher _passwordHasher;


    public AuthControllerInMemoryTests(IntegrationTestFixture testFixture)
    {
        _dbContext = testFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _passwordHasher = testFixture.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var loggerMock = new Mock<ILogger<AuthController>>();
        var sender = testFixture.ServiceProvider.GetRequiredService<ISender>();
        _authController = new AuthController(loggerMock.Object, sender);
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();

        var user = User.Create(Username.Create("TestUser").Value,
                               Password.Create(_passwordHasher.HashPassword("Test-Pa55word")).Value,
                               Email.Create("testemail@test.de").Value);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
    }


    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var request = new LoginRequest("TestUser", "Test-Pa55word");

        var result = await _authController.Login(request, CancellationToken.None);// as OkObjectResult;

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult)!.Value.Should().BeOfType<LoginResponse>();
    }

    [Fact]
    public async Task Login_Should_Return_BadRequest_When_Credentials_Are_Invalid()
    {
        var request = new LoginRequest("TestUser", "Wrong-Pa55word");

        var result = await _authController.Login(request, CancellationToken.None) as BadRequestObjectResult;

        result.Should().NotBeNull();
        var problemDetails = result!.Value as ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Detail.Should().Be(UserErrors.WrongLoginCredientials.Message);
    }


}
