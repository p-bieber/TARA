using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TARA.AuthenticationService.Api.Controllers;
using TARA.AuthenticationService.Api.Dtos;
using TARA.AuthenticationService.Application.Users.Login;
using TARA.AuthenticationService.Domain.Users;
using TARA.AuthenticationService.Domain.Users.DomainEvents;
using TARA.AuthenticationService.Domain.Users.Errors;
using TARA.AuthenticationService.Domain.Users.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Data;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.AuthenticationService.Tests.Fixtures;

namespace TARA.AuthenticationService.Tests.IntegrationsTests;

public class UserControllerInMemoryTests : IAsyncLifetime, IClassFixture<IntegrationTestFixture>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserController _userController;
    private readonly IPasswordHasher _passwordHasher;


    public UserControllerInMemoryTests(IntegrationTestFixture testFixture)
    {
        _dbContext = testFixture.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _passwordHasher = testFixture.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var loggerMock = new Mock<ILogger<UserController>>();
        var sender = testFixture.ServiceProvider.GetRequiredService<ISender>();
        _userController = new UserController(loggerMock.Object, sender);
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
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
    public async Task Register_Should_Return_Ok_When_Data_Is_Valid_And_Insert_User_In_Database()
    {
        var request = new RegisterUserRequest("TestUser2", "Test-Pa55word", "testemai2l@test.de");

        var result = await _userController.Register(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtActionResult>();
        (result as CreatedAtActionResult)!.StatusCode.Should().Be(StatusCodes.Status201Created);


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
        var request = new RegisterUserRequest(username, password, email);

        var result = await _userController.Register(request, CancellationToken.None);
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Login_Should_Return_Ok_When_Credentials_Are_Valid()
    {
        var request = new LoginRequest("TestUser", "Test-Pa55word");

        var result = await _userController.Login(request, CancellationToken.None);// as OkObjectResult;

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult)!.Value.Should().BeOfType<LoginResponse>();
    }

    [Fact]
    public async Task Login_Should_Return_BadRequest_When_Credentials_Are_Invalid()
    {
        var request = new LoginRequest("TestUser", "Wrong-Pa55word");

        var result = await _userController.Login(request, CancellationToken.None) as BadRequestObjectResult;

        result.Should().NotBeNull();
        var problemDetails = result!.Value as ProblemDetails;
        problemDetails.Should().NotBeNull();
        problemDetails!.Detail.Should().Be(UserErrors.WrongLoginCredientials.Message);
    }
}
