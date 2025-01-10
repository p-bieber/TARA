using FluentAssertions;
using Moq;
using TARA.AuthenticationService.Application.Services;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;

namespace TARA.AuthenticationService.Tests.UnitTests.Services;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateUser_Should_Return_True_Whem_Credentials_Are_Valid()
    {
        var username = "Maxim";
        var password = "12345";
        var email = "max@mustermann.de";
        var user = User.Create(username, password, email);

        _userRepositoryMock.Setup(x => x.GetUserIdAsync(It.IsAny<string>())).ReturnsAsync("TESTID");
        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

        var result = await _userService.ValidateUserAsync(username, password);

        result.Item1.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateUser_Should_Return_False_When_Credentials_Are_Invalid()
    {
        var result = await _userService.ValidateUserAsync("username", "password");
        result.Item1.Should().BeFalse();
    }
}
