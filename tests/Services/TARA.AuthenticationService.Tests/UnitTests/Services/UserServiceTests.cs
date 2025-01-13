using FluentAssertions;
using Moq;
using TARA.AuthenticationService.Application.Services;
using TARA.AuthenticationService.Domain;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;
using TARA.Shared;

namespace TARA.AuthenticationService.Tests.UnitTests.Services;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _userService = new UserService(_userRepositoryMock.Object,
                                       _passwordHasherMock.Object);
    }
    [Fact]
    public async Task GetUserByName_Should_Return_User_When_User_Exists()
    {
        var username = "Maxim";

        var user = User.Create(Username.Create(username), Password.Create("password"), Email.Create("max@mustermann.de"));

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(username)).ReturnsAsync(Result.Success(user));

        var result = await _userService.GetUserByNameAsync(username);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().Be(user);
    }

    [Fact]
    public async Task GetUserByName_Should_Return_Failure_When_User_Not_Exists()
    {
        var username = "Maxim";

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(username)).ReturnsAsync(Result.Failure<User>(AppErrors.UserError.NotFound));

        var result = await _userService.GetUserByNameAsync(username);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AppErrors.UserError.NotFound);
    }

    [Fact]
    public async Task VerifyUserPassword_Should_Return_Success_When_Credentials_Are_Valid()
    {
        var username = "Maxim";
        var password = "12345";
        var email = "max@mustermann.de";
        var user = User.Create(Username.Create(username), Password.Create(password), Email.Create(email));

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(username)).ReturnsAsync(Result.Success(user));
        _passwordHasherMock.Setup(x => x.VerifyPassword(password, It.IsAny<string>())).Returns(true);

        var result = await _userService.VerifyUserPasswordAsync(username, password);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
    [Fact]
    public async Task VerifyUserPassword_Should_Return_Failure_When_Password_Is_Invalid()
    {
        var username = "Maxim";
        var password = "12345";
        var email = "max@mustermann.de";
        var user = User.Create(Username.Create(username), Password.Create(password), Email.Create(email));

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(username)).ReturnsAsync(Result.Success(user));
        _passwordHasherMock.Setup(x => x.VerifyPassword(password, It.IsAny<string>())).Returns(false);

        var result = await _userService.VerifyUserPasswordAsync(username, password);

        result.Should().NotBeNull();
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(AppErrors.UserError.WrongLoginCredientials);
    }
    [Fact]
    public async Task VerifyUserPassword_Should_Return_Failure_When_User_NotFound()
    {
        var username = "Maxim";
        var password = "12345";

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(username)).ReturnsAsync(Result.Failure<User>(AppError.NotFound));

        var result = await _userService.VerifyUserPasswordAsync(username, password);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AppErrors.UserError.WrongLoginCredientials);
    }
}
