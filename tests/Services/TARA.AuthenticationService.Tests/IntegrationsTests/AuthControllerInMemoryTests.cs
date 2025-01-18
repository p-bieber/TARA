using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Application.Users.Create;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.AuthenticationService.Domain.Users.DomainEvents;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Tests.Fixtures;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;

public class AuthControllerInMemoryTests : IAsyncLifetime, IClassFixture<IntegrationTestFixture>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly AuthController _authController;


    public AuthControllerInMemoryTests(IntegrationTestFixture testFixture)
    {
        _dbContext = testFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var loggerMock = new Mock<ILogger<AuthController>>();
        var sender = testFixture.ServiceProvider.GetRequiredService<ISender>();
        _authController = new AuthController(loggerMock.Object, sender);
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task Register_Should_Return_Ok_When_Data_Is_Valid_And_Insert_User_In_Database()
    {
        var request = new CreateUserCommand("TestUser", "Test-Pa55word", "testemail@test.de");

        var result = await _authController.Register(request) as OkResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username.Value == "TestUser");
        user.Should().NotBeNull();
        user?.Email.Value.Should().Be("testemail@test.de");

        var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.AggregateId == user!.Id);
        @event.Should().NotBeNull();
        @event!.Type.Should().Be(nameof(UserCreatedDomainEvent));
    }

    [Theory]
    [InlineData("TestUser", "Test-Password", "testemail@test.de")]
    [InlineData("TestUser", "P4ss", "testemail@test.de")]
    [InlineData("TestUser", "test-pa55word", "testemail@test.de")]
    [InlineData("TestUser", "test-password", "testemail@test.de")]
    [InlineData("TestUser", "Test-Pa55word", "testemailtest.de")]
    public async Task Register_Should_Return_BadRequest_With_ErrorMsg_When_Data_Is_Invalid_And_Dont_Insert_User_In_Database(string username, string password, string email)
    {
        var request = new CreateUserCommand(username, password, email);

        var result = await _authController.Register(request) as BadRequestObjectResult;
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var createRequest = new CreateUserCommand("TestUser", "Test-Pa55word", "testemail@test.de");
        await _authController.Register(createRequest);

        var request = new LoginQuery("TestUser", "Test-Pa55word");
        var result = await _authController.Login(request) as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeOfType<LoginResponse>();
    }

    [Fact]
    public async Task Login_Should_Return_BadRequest_When_Credentials_Are_Invalid()
    {
        var createRequest = new CreateUserCommand("TestUser", "Test-Pa55word", "testemail@test.de");
        await _authController.Register(createRequest);

        var request = new LoginQuery("TestUser", "Wrong-Pa55word");
        var result = await _authController.Login(request) as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeOfType<Error>();
        result.Value.Should().Be(UserErrors.WrongLoginCredientials);
    }


}
