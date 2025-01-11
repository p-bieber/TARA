using FluentAssertions;
using Moq;
using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Application.Services;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

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

        var userNameFactory = new UserNameFactory(new UserNameValidator());
        var passwordFactory = new PasswordFactory(new PasswordValidator(), _passwordHasherMock.Object);
        var emailFactory = new EmailFactory(new EmailValidator());

        var userFactory = new UserFactory(_passwordHasherMock.Object);
        _userService = new UserService(_userRepositoryMock.Object,
                                       _passwordHasherMock.Object);
    }

    [Fact]
    public async Task ValidateUser_Should_Return_True_Whem_Credentials_Are_Valid()
    {
        var username = "Maxim";
        var password = "12345";
        var email = "max@mustermann.de";
        var user = User.Create(UserName.Create(username), Password.Create(password), Email.Create(email));

        _userRepositoryMock.Setup(x => x.GetUserByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _passwordHasherMock.Setup(x => x.VerifyPassword(password, It.IsAny<string>())).Returns(true);

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
